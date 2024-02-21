using System.Runtime.Serialization;
using TracerLib.Model;

namespace TracerLib.Serialization;

public interface ISerializer
{
    string Serialize(TraceResult traceResult);
}