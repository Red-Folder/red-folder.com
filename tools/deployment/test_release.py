import hashlib
import io
import json
import os
from pathlib import Path
import tempfile
import unittest
from unittest.mock import patch
import zipfile

import release


COMMIT = "a" * 40
REPOSITORY = "Red-Folder/red-folder.com"


def source_run(**changes):
    result = dict(id=12, head_sha=COMMIT, head_branch="master", head_repository={"full_name": REPOSITORY},
                  path=".github/workflows/azure-deploy.yml", event="push", status="completed", conclusion="success", run_number=7)
    result.update(changes)
    return result


def package(metadata=None, extra=None):
    result = io.BytesIO()
    with zipfile.ZipFile(result, "w") as archive:
        archive.writestr("version.json", json.dumps(metadata or dict(commitSha=COMMIT, branchName="master", buildNumber="7")))
        archive.writestr("RedFolder.com.dll", b"test application")
        if extra:
            entry, content = extra
            if isinstance(entry, str):
                # ZipInfo normally normalizes backslashes on Windows; preserve an
                # untrusted archive's exact spelling for the cross-platform test.
                raw_entry = zipfile.ZipInfo()
                raw_entry.filename = entry
                entry = raw_entry
            archive.writestr(entry, content)
    return result.getvalue()


def jobs():
    return {"jobs": [
        {"conclusion": "success", "steps": [
            {"name": "Run Tests", "conclusion": "success"},
            {"name": "Upload artifact for deployment job", "conclusion": "success"}]},
        {"conclusion": "success", "steps": [
            {"name": "Deploy to Azure Web App", "conclusion": "success"},
            {"name": "Verify production deployment", "conclusion": "success"}]}]}


class ReleaseTests(unittest.TestCase):
    def setUp(self):
        self.environment = patch.dict(os.environ, GITHUB_REPOSITORY=REPOSITORY, GITHUB_SHA=COMMIT,
                                      GITHUB_REF="refs/heads/master", GITHUB_RUN_NUMBER="7", GITHUB_RUN_ID="12")
        self.environment.start()
        self.addCleanup(self.environment.stop)
        self.directory = tempfile.TemporaryDirectory()
        self.addCleanup(self.directory.cleanup)
        self.destination = Path(self.directory.name) / "package"

    def test_rejects_untrusted_or_failed_source_runs(self):
        for changes in [dict(head_sha="b" * 40), dict(head_branch="feature"), dict(event="pull_request"),
                        dict(conclusion="failure"), dict(status="in_progress"), dict(path="other.yml"),
                        dict(head_repository={"full_name": "attacker/fork"})]:
            with self.subTest(changes=changes), self.assertRaises(ValueError):
                release.validate_run(source_run(**changes), COMMIT)

    def test_successful_workflow_without_production_verification_is_not_a_baseline(self):
        evidence = jobs()
        evidence["jobs"][1]["steps"][1]["conclusion"] = "skipped"
        with patch.object(release, "api", return_value=evidence), self.assertRaises(ValueError):
            release.verified_run(source_run(), COMMIT)

    def test_failed_tests_cannot_produce_verified_baseline(self):
        evidence = jobs()
        evidence["jobs"][0]["steps"][0]["conclusion"] = "failure"
        with patch.object(release, "api", return_value=evidence), self.assertRaises(ValueError):
            release.verified_run(source_run(), COMMIT)

    def test_accepts_successful_tested_and_production_verified_run(self):
        with patch.object(release, "api", return_value=jobs()):
            release.verified_run(source_run(), COMMIT)

    def test_extracts_real_application_assembly_contract(self):
        release.extract_package(package(), self.destination, COMMIT, 7)
        self.assertTrue((self.destination / "RedFolder.com.dll").is_file())

    def test_rejects_metadata_mismatch_before_writing(self):
        for metadata in [dict(commitSha="b" * 40, branchName="master", buildNumber="7"),
                         dict(commitSha=COMMIT, branchName="feature", buildNumber="7"),
                         dict(commitSha=COMMIT, branchName="master", buildNumber="8")]:
            with self.subTest(metadata=metadata), self.assertRaises(ValueError):
                release.extract_package(package(metadata), self.destination, COMMIT, 7)
        self.assertFalse(self.destination.exists())

    def test_rejects_archive_traversal_and_absolute_paths(self):
        for name in ["../escaped", "/escaped", "C:/escaped", "nested\\escaped"]:
            with self.subTest(name=name), self.assertRaises(ValueError):
                release.extract_package(package(extra=(name, b"bad")), self.destination, COMMIT, 7)
        self.assertFalse(self.destination.exists())

    def test_rejects_archive_symlink(self):
        entry = zipfile.ZipInfo("link")
        entry.external_attr = 0o120777 << 16
        with self.assertRaises(ValueError):
            release.extract_package(package(extra=(entry, "outside")), self.destination, COMMIT, 7)

    def test_candidate_content_change_fails_staging_fingerprint(self):
        release.extract_package(package(), self.destination, COMMIT, 7)
        with patch.object(release, "output") as output, patch.object(release, "summary"):
            release.candidate(self.destination)
            digest = output.call_args.args[1]
            (self.destination / "RedFolder.com.dll").write_bytes(b"changed")
            with self.assertRaisesRegex(ValueError, "differs"):
                release.candidate(self.destination, digest)

    def test_candidate_identical_package_matches_staging(self):
        release.extract_package(package(), self.destination, COMMIT, 7)
        with patch.object(release, "output") as output, patch.object(release, "summary"):
            release.candidate(self.destination)
            release.candidate(self.destination, output.call_args.args[1])

    def test_rejects_unsafe_or_empty_staging_origins(self):
        for url in ["", "http://example.com", "https://user:secret@example.com", "https://example.com/path",
                    "https://example.com?query=value", "https://example.com#fragment"]:
            with self.subTest(url=url), self.assertRaises(ValueError):
                release.validate_origin(url)

    def test_current_guard_skips_superseded_release(self):
        with patch.object(release, "api", return_value={"object": {"sha": "b" * 40}}), \
                patch.object(release, "output") as output, patch.object(release, "summary"):
            release.current()
            output.assert_called_once_with("deploy", "false")
            with self.assertRaises(ValueError):
                release.current(strict=True)

    def prepare_with(self, artifact_changes=None, download=None):
        data = package()
        artifact = dict(id=20, name=".net-app", expired=False, digest="sha256:" + hashlib.sha256(data).hexdigest())
        artifact.update(artifact_changes or {})
        def api(path):
            if path.startswith("compare/"):
                return {"status": "ahead"}
            if path == "actions/runs/12":
                return source_run()
            if "/jobs?" in path:
                return jobs()
            if "/artifacts?" in path:
                return {"artifacts": [artifact]}
            raise AssertionError(path)
        with patch.object(release, "api", side_effect=api), patch.object(release, "gh", return_value=download or data), \
                patch.object(release, "output"), patch.object(release, "summary"):
            release.prepare(self.destination, COMMIT, "12")

    def test_prepares_verified_digest_checked_baseline(self):
        self.prepare_with()
        self.assertTrue(self.destination.exists())

    def test_expired_artifact_fails_before_mutation(self):
        with self.assertRaisesRegex(ValueError, "retained"):
            self.prepare_with(dict(expired=True))
        self.assertFalse(self.destination.exists())

    def test_missing_artifact_digest_fails_closed(self):
        with self.assertRaisesRegex(ValueError, "digest"):
            self.prepare_with(dict(digest=""))

    def test_corrupted_download_fails_before_extraction(self):
        with self.assertRaisesRegex(ValueError, "digest mismatch"):
            self.prepare_with(download=b"tampered archive")
        self.assertFalse(self.destination.exists())

    def test_readiness_retries_startup_but_requires_exact_version(self):
        with patch.object(release, "read_json", return_value={"status": "Healthy"}), \
                patch.object(release, "live_sha", side_effect=["b" * 40, COMMIT]), \
                patch.object(release.time, "sleep") as sleep:
            release.ready("https://example.com", COMMIT)
            sleep.assert_called_once_with(5)


if __name__ == "__main__":
    unittest.main()
