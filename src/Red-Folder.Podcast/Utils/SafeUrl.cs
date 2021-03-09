using System.Text.RegularExpressions;

namespace RedFolder.Podcast.Utils
{
    public static class SafeUrl
    {
        private const string ALPHA_LOWER = "abcdefghijklmnopqrstuvwxyz";
        private const string ALPHA_UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string NUMERIC = "0123456789";
        private const string SPACE = " ";

        public static string MakeSafe(string source)
        {
            var pattern = string.Concat(ALPHA_LOWER, ALPHA_UPPER, NUMERIC, SPACE);
            var cleaned = Regex.Replace(source, $"[^{pattern}]", "");

            return cleaned.Replace(" ", "-");
        }
    }
}
