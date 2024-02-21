using System.Xml.Serialization;

namespace TracerLib.Dto.Xml;

[XmlType("method")]
public class XmlMethodTraceResultDto
{
    [XmlAttribute("name")]
    public string Name { get; set; }
    [XmlAttribute("class")]
    public string Class { get; set; }
    [XmlAttribute("time")]
    public string Time { get; set; }
    [XmlElement("method")]
    public List<XmlMethodTraceResultDto> Methods { get; set; }
}