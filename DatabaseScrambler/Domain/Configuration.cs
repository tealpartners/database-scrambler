using System.Xml.Serialization;

namespace DatabaseScrambler.Domain
{
    public class Configuration
    {
        [XmlAttribute("type")]
        public ScrambleType Type { get; set; }

        [XmlAttribute("tableName")]
        public string TableName { get; set; }

        [XmlAttribute("columnName")]
        public string ColumnName { get; set; }

        [XmlAttribute("identifier")]
        public string Identifier { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }
    }
}
