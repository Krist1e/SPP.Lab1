using System.Globalization;
using TracerLib.Model;

namespace TracerLib.Dto.Xml;

public class XmlDtoMapper
{
    public static XmlTraceResultDto Map(TraceResult traceResult)
    {
        var threads = traceResult.Threads.Select(Map).ToList();
        return new XmlTraceResultDto()
        {
            Threads = threads
        };
    }

    private static XmlThreadTraceResultDto Map(ThreadTraceResult threadTraceResult)
    {
        var methods = threadTraceResult.Methods.Select(Map).ToList();
        return new XmlThreadTraceResultDto()
        {
            ThreadId = threadTraceResult.ThreadId,
            Time = threadTraceResult.TotalTimeInMilliseconds.TotalMilliseconds.ToString(CultureInfo.InvariantCulture) + "ms",
            Methods = methods
        };
    }

    private static XmlMethodTraceResultDto Map(MethodTraceResult methodTraceResult)
    {
        var methods = methodTraceResult.InnerMethods.Select(Map).ToList();
        return new XmlMethodTraceResultDto()
        {
            Name = methodTraceResult.MethodName,
            Class = methodTraceResult.ClassName,
            Time = methodTraceResult.TimeInMilliseconds.TotalMilliseconds.ToString(CultureInfo.InvariantCulture) + "ms",
            Methods = methods
        };
    }
}