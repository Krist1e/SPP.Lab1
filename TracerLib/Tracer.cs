using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection;

namespace TracerLib;

public class Tracer : ITracer<TraceResult>
{
    private TraceResult _traceResult;

    public Tracer()
    {
        _traceResult = new TraceResult();
    }
    public void StartTrace()
    {
        MethodBase method = new StackTrace().GetFrame(1)!.GetMethod()!;
        _traceResult.StartTrace(Thread.CurrentThread.ManagedThreadId, new MethodTraceResult(method));
    }

    public void StopTrace() 
    {
        _traceResult.StopTrace(Thread.CurrentThread.ManagedThreadId);
    }

    public TraceResult GetTraceResult()
    {
        return _traceResult;
    }
}