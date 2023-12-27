using HtmlAgilityPack;
using System.IO;

namespace RedFolder.WebSite.Integration.Tests.Utils
{
    public class HtmlFromatter
    {
        public static string FormatHtml(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            using (var stringWriter = new StringWriter())
            {
                doc.Save(stringWriter);
                return stringWriter.ToString();
            }
        }
    }
}
