using System.IO;
using System.Xml;

namespace RedFolder.WebSite.Integration.Tests.Utils
{
    public class XmlFormatter
    {
        public static string FormatXml(string xml)
        {
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);

                using var stringWriter = new StringWriter();
                using var xmlTextWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true });
                xmlDoc.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();

                return stringWriter.GetStringBuilder().ToString();
            }
            catch (XmlException)
            {
                // Handle or throw the exception if the XML is invalid
                throw;
            }
        }
    }
}
