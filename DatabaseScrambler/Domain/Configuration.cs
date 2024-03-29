﻿using System.Xml.Serialization;

namespace DatabaseScrambler.Domain
{
    public class Configuration
    {
        [XmlAttribute("type")]
        public ScrambleType Type { get; set; }

        [XmlAttribute("schema")]
        public string Schema { get; set; } = "dbo";

        [XmlAttribute("tableName")]
        public string TableName { get; set; }

        [XmlAttribute("columnName")]
        public string ColumnName { get; set; }

        [XmlAttribute("identifier")]
        public string Identifier { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }

        [XmlAttribute("copyColumn")]
        public string CopyColumn { get; set; }
    }
}