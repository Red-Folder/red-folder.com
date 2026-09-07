"""Fail-closed artifact selection and bounded readiness for GitHub deployment jobs.

Only this checked-out script is executed; historical artifacts are deployment data.
Uses Python's standard library and the runner's authenticated GitHub CLI.
"""
import argparse
import hashlib
import io
import json
import os
from pathlib import Path, PurePosixPath
import re
import subprocess
import sys
import time
import urllib.request
from urllib.parse import urlsplit
import zipfile


SHA = re.compile(r"[0-9a-f]{40}")


def require(condition, message):
    if not condition:
        raise ValueError(message)


def gh(path, binary=False):
    result = subprocess.run(["gh", "api", path], capture_output=True, check=False)
    require(result.returncode == 0, "GitHub API request failed; no deployment is authorized.")
    return result.stdout if binary else json.loads(result.stdout)


def api(path):
    return gh(f"repos/{os.environ['GITHUB_REPOSITORY']}/{path}")


class NoRedirect(urllib.request.HTTPRedirectHandler):
    def redirect_request(self, req, fp, code, msg, headers, newurl):
        return None


def read_json(url):
    require(url.startswith("https://"), "Deployment checks require HTTPS.")
    request = urllib.request.Request(url, headers={"Cache-Control": "no-cache"})
    with urllib.request.build_opener(NoRedirect).open(request, timeout=15) as response:
        require(response.status == 200, "Endpoint did not return HTTP 200.")
        return json.load(response)


def validate_origin(url):
    parsed = urlsplit(url)
    require(parsed.scheme == "https" and parsed.hostname and not parsed.username and not parsed.password and
            parsed.path in ("", "/") and not parsed.query and not parsed.fragment,
            "Deployment URL must be an HTTPS origin without credentials, path, query or fragment.")


def live_sha(url):
    validate_origin(url)
    value = read_json(url.rstrip("/") + "/api/version").get("commitSha", "")
    require(isinstance(value, str) and SHA.fullmatch(value), "Live version is missing a full commit SHA.")
    return value


def output(name, value):
    with open(os.environ["GITHUB_OUTPUT"], "a", encoding="utf-8") as stream:
        stream.write(f"{name}={value}\n")


def summary(message):
    with open(os.environ["GITHUB_STEP_SUMMARY"], "a", encoding="utf-8") as stream:
        stream.write(message + "\n")


def current(strict=False):
    latest = api("git/ref/heads/master")["object"]["sha"]
    matches = os.environ["GITHUB_REF"] == "refs/heads/master" and latest == os.environ["GITHUB_SHA"]
    if strict:
        require(matches, "Dispatch recovery/rehearsal on current master; obsolete runs must not be retried.")
    output("deploy", str(matches).lower())
    if not matches:
        summary("Deployment skipped: candidate is no longer current master.")


def validate_run(run, expected):
    require(SHA.fullmatch(expected) is not None, "Expected SHA must be 40 lowercase hexadecimal characters.")
    require(run.get("head_sha") == expected, "Source run commit does not match expected SHA.")
    require(run.get("head_branch") == "master", "Source must be a master run.")
    require(run.get("head_repository", {}).get("full_name") == os.environ["GITHUB_REPOSITORY"], "Source repository mismatch.")
    require(run.get("path") == ".github/workflows/azure-deploy.yml", "Source must use the production deployment workflow.")
    require(run.get("event") in ("push", "workflow_dispatch"), "Untrusted source event.")
    require(run.get("status") == "completed" and run.get("conclusion") == "success", "Source run must have completed successfully.")


def verified_run(run, expected):
    validate_run(run, expected)
    jobs = api(f"actions/runs/{run['id']}/jobs?filter=latest&per_page=100")["jobs"]
    require(any(job.get("conclusion") == "success" and
                any(step.get("name") == "Run Tests" and step.get("conclusion") == "success" for step in job.get("steps", [])) and
                any(step.get("name") == "Upload artifact for deployment job" and step.get("conclusion") == "success" for step in job.get("steps", []))
                for job in jobs), "Source run lacks successful tests and deployment artifact creation.")
    require(any(job.get("conclusion") == "success" and
                any(step.get("name") == "Deploy to Azure Web App" and step.get("conclusion") == "success" for step in job.get("steps", [])) and
                any(step.get("name") == "Verify production deployment" and step.get("conclusion") == "success" for step in job.get("steps", []))
                for job in jobs), "Source run has no successful production deployment and verification evidence.")


def extract_package(data, destination, expected, run_number):
    destination = Path(destination)
    require(not destination.exists(), "Artifact destination must not already exist.")
    with zipfile.ZipFile(io.BytesIO(data)) as archive:
        names = archive.namelist()
        require(len(names) == len(set(names)), "Duplicate artifact entries.")
        for item in archive.infolist():
            path = PurePosixPath(item.filename)
            require(not path.is_absolute() and ".." not in path.parts and "\\" not in item.orig_filename and ":" not in item.filename,
                    "Unsafe artifact path.")
            require((item.external_attr >> 16) & 0o170000 != 0o120000, "Artifact symlinks are not supported.")
        require("version.json" in names, "Artifact lacks root version metadata.")
        metadata = json.loads(archive.read("version.json"))
        require(metadata.get("commitSha") == expected and metadata.get("branchName") == "master" and
                str(metadata.get("buildNumber")) == str(run_number), "Artifact version metadata does not match its source run.")
        require("RedFolder.com.dll" in names, "Artifact lacks the application assembly.")
        destination.mkdir(parents=True)
        archive.extractall(destination)


def prepare(destination, expected=None, run_id=None, url=None):
    expected = expected or live_sha(url)
    require(SHA.fullmatch(expected) is not None, "Invalid expected SHA.")
    comparison = api(f"compare/{expected}...master")
    require(comparison.get("status") in ("ahead", "identical"), "Source commit is not an ancestor of master.")
    if run_id:
        require(run_id.isdecimal(), "Source run ID must be numeric.")
        run = api(f"actions/runs/{run_id}")
        verified_run(run, expected)
    else:
        runs = api(f"actions/workflows/azure-deploy.yml/runs?branch=master&status=success&head_sha={expected}&per_page=100")["workflow_runs"]
        run = None
        for candidate in runs:
            try:
                verified_run(candidate, expected)
                run = candidate
                break
            except ValueError:
                continue
        require(run is not None, "No successful production-verified master run found for the live SHA; restore release evidence before deployment.")
    artifacts = api(f"actions/runs/{run['id']}/artifacts?per_page=100")["artifacts"]
    matches = [artifact for artifact in artifacts if artifact["name"] == ".net-app" and not artifact["expired"]]
    require(len(matches) == 1, "Exactly one retained .net-app artifact is required; renew the known-good release before retention expires.")
    artifact = matches[0]
    digest = artifact.get("digest", "")
    require(re.fullmatch(r"sha256:[0-9a-f]{64}", digest) is not None, "Artifact has no verifiable SHA-256 digest.")
    data = gh(f"repos/{os.environ['GITHUB_REPOSITORY']}/actions/artifacts/{artifact['id']}/zip", binary=True)
    require("sha256:" + hashlib.sha256(data).hexdigest() == digest, "Artifact digest mismatch.")
    extract_package(data, destination, expected, run["run_number"])
    output("sha", expected)
    output("run_id", run["id"])
    summary(f"Prepared recovery artifact: commit `{expected}`, run `{run['id']}`, artifact `{artifact['id']}`, digest `{digest}`.")


def ready(url, expected):
    validate_origin(url)
    require(SHA.fullmatch(expected) is not None, "Invalid readiness SHA.")
    # Retry startup/version readiness only; the substantive smoke suite runs once afterwards.
    deadline = time.monotonic() + 120
    while time.monotonic() < deadline:
        try:
            if read_json(url.rstrip("/") + "/health").get("status") == "Healthy" and live_sha(url) == expected:
                return
        except (ValueError, OSError):
            pass
        time.sleep(5)
    raise ValueError("Readiness/version did not converge within the bounded warm-up period.")


def candidate(directory, expected_digest=None):
    root = Path(directory)
    metadata = json.loads((root / "version.json").read_text(encoding="utf-8"))
    require(metadata.get("commitSha") == os.environ["GITHUB_SHA"] and metadata.get("branchName") == "master" and
            str(metadata.get("buildNumber")) == os.environ["GITHUB_RUN_NUMBER"], "Candidate metadata mismatch.")
    require((root / "RedFolder.com.dll").is_file(), "Candidate lacks the application assembly.")
    digest = hashlib.sha256()
    for file in sorted(root.rglob("*")):
        require(not file.is_symlink(), "Candidate symlink is unsupported.")
        if file.is_file():
            digest.update(file.relative_to(root).as_posix().encode("utf-8") + b"\0")
            digest.update(hashlib.sha256(file.read_bytes()).digest())
    value = digest.hexdigest()
    require(expected_digest is None or value == expected_digest, "Candidate differs from the package verified in staging.")
    output("digest", value)
    summary(f"Candidate `{os.environ['GITHUB_SHA']}`, run `{os.environ['GITHUB_RUN_ID']}`, package SHA-256 `{value}`.")


def main():
    parser = argparse.ArgumentParser()
    sub = parser.add_subparsers(dest="command", required=True)
    check = sub.add_parser("current")
    check.add_argument("--strict", action="store_true")
    origin = sub.add_parser("origin")
    origin.add_argument("url")
    previous = sub.add_parser("prepare")
    previous.add_argument("--destination", required=True)
    previous.add_argument("--sha")
    previous.add_argument("--run-id")
    previous.add_argument("--url")
    readiness = sub.add_parser("ready")
    readiness.add_argument("url")
    readiness.add_argument("sha")
    package = sub.add_parser("candidate")
    package.add_argument("directory")
    package.add_argument("--expected-digest")
    args = parser.parse_args()
    if args.command == "current":
        current(args.strict)
    elif args.command == "origin":
        validate_origin(args.url)
    elif args.command == "prepare":
        require(bool(args.url) != bool(args.sha), "Supply either live URL or explicit SHA.")
        prepare(args.destination, args.sha, args.run_id, args.url)
    elif args.command == "candidate":
        candidate(args.directory, args.expected_digest)
    else:
        ready(args.url, args.sha)


if __name__ == "__main__":
    try:
        main()
    except Exception as error:
        # Do not print URLs, response bodies, subprocess output or credentials.
        print(f"::error::{str(error) if type(error) is ValueError else 'Release verification failed; inspect configuration and release evidence.'}", file=sys.stderr)
        sys.exit(1)
