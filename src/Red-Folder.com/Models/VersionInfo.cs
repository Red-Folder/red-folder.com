using System;

namespace RedFolder.Models
{
    /// <summary>
    /// Contains version and build information for the application
    /// </summary>
    public class VersionInfo
    {
        /// <summary>
        /// Git commit SHA that was deployed
        /// </summary>
        public string CommitSha { get; set; }

        /// <summary>
        /// Short version of the commit SHA (first 7 characters)
        /// </summary>
        public string ShortCommitSha { get; set; }

        /// <summary>
        /// Branch name from which the build was created
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// Date and time when the build was created
        /// </summary>
        public string BuildTime { get; set; }

        /// <summary>
        /// GitHub Actions run number
        /// </summary>
        public string BuildNumber { get; set; }

        /// <summary>
        /// Link to the commit on GitHub
        /// </summary>
        public string CommitUrl { get; set; }
    }
}
