using System.Xml.Serialization;
using TracerLib.Model;

namespace TracerLib.Dto.Xml;

[XmlRoot("root")]
public class XmlTraceResultDto
{
    [XmlElement("thread")]
    public List<XmlThreadTraceResultDto> Threads { get; set; }
    
}