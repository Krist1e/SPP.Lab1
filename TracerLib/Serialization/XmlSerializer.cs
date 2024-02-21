using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using TracerLib.Dto.Xml;
using TracerLib.Model;

namespace TracerLib.Serialization;

public class XmlSerializer : ISerializer
{
    public string Serialize(TraceResult traceResult)
    {
        var dto = XmlDtoMapper.Map(traceResult);
        var xmlSettings = new XmlWriterSettings
        {
            Indent = true,
            IndentChars = "    "
        };
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(XmlTraceResultDto));
        var sb = new StringBuilder();
        using (var writer = XmlWriter.Create(sb, xmlSettings))
        {
            serializer.Serialize(writer, dto);
        }
        return sb.ToString();
    }
}