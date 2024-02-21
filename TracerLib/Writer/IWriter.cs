using TracerLib.Model;

namespace TracerLib.Writer;

public interface IWriter
{
    void Write(Stream stream, TraceResult traceResult);
}