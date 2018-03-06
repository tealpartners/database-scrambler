using System.Collections.Generic;
using System.Xml.Serialization;

namespace DatabaseScrambler.Domain
{
    [XmlRoot("root")]
    public class XmlConfiguration
    {
        [XmlElement("scramble")]
        public List<Configuration> ConfigurationItems { get; set; }
    }
}
