using TracerLib.Model;
using TracerLib.Serialization;

namespace TracerLib.Writer;

public class Writer(ISerializer serializer) : IWriter
{
    public void Write(Stream stream, TraceResult traceResult)
    {
        string serializedData = serializer.Serialize(traceResult);
        using (var writer = new StreamWriter(stream))
        {
            writer.Write(serializedData);
        }
    }
}