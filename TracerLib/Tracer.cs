using System.Diagnostics;

namespace TracerLib;

public class Tracer : ITracer<TraceResult>
{
    private readonly Stopwatch _stopwatch;
    private TraceResult _traceResult;

    public Tracer()
    {
        _stopwatch = new Stopwatch();
        _traceResult = new TraceResult();
    }
    public void StartTrace()
    {
        StackTrace stackTrace = new StackTrace(false);
        var frame = stackTrace.GetFrame(1);
        var method = frame?.GetMethod()?.Name;
        var className = frame?.GetMethod()?.ReflectedType?.Name;
        _traceResult.ClassName = className ?? "";
        _traceResult.MethodName = method ?? "";
        if (frame != null) Console.WriteLine(" Class: {0} Method: {1}", className, method);
        _stopwatch.Start();
    }

    public void StopTrace() 
    {
        _stopwatch.Stop();
        _traceResult.TimeInMilliseconds = _stopwatch.ElapsedMilliseconds;
        _traceResult.methods.Add(_traceResult);
    }

    public TraceResult GetTraceResult()
    {
        return _traceResult;
    }
}