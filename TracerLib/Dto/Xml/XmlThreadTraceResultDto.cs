using System.Xml.Serialization;

namespace TracerLib.Dto.Xml;

[XmlType("thread")]
public class XmlThreadTraceResultDto
{
    [XmlAttribute("id")]
    public int ThreadId { get; set; }
    [XmlAttribute("time")]
    public string Time { get; set; }
    [XmlElement("method")]
    public List<XmlMethodTraceResultDto> Methods { get; set; }
}