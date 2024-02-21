using System.Globalization;
using TracerLib.Dto.Json;
using TracerLib.Dto.Xml;
using TracerLib.Model;

namespace TracerLib.Mapper;

public class JsonDtoMapper
{
    public static JsonTraceResultDto Map(TraceResult traceResult)
    {
        var threads = traceResult.Threads.Select(Map).ToList();
        return new JsonTraceResultDto()
        {
            Threads = threads
        };
    }

    private static JsonThreadTraceResultDto Map(ThreadTraceResult threadTraceResult)
    {
        var methods = threadTraceResult.Methods.Select(Map).ToList();
        return new JsonThreadTraceResultDto()
        {
            ThreadId = threadTraceResult.ThreadId,
            Time = threadTraceResult.TotalTimeInMilliseconds.TotalMilliseconds.ToString(CultureInfo.InvariantCulture) + "ms",
            Methods = methods
        };
    }

    private static JsonMethodTraceResultDto Map(MethodTraceResult methodTraceResult)
    {
        var methods = methodTraceResult.InnerMethods.Select(Map).ToList();
        return new JsonMethodTraceResultDto()
        {
            Name = methodTraceResult.MethodName,
            Class = methodTraceResult.ClassName,
            Time = methodTraceResult.TimeInMilliseconds.TotalMilliseconds.ToString(CultureInfo.InvariantCulture) + "ms",
            Methods = methods
        };
    }
}