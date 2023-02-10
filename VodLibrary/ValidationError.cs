using System.Xml.Schema;

namespace VodLibrary
{
    public class ValidationError
    {
        public XmlSeverityType Severity { get; set; } = XmlSeverityType.Warning;
        public String Error { get; set; } = string.Empty;
    }
}
