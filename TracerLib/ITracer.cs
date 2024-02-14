namespace TracerLib;

public interface ITracer
{
    void StartTrace();
    void StopTrace();
    TraceResult GetTraceResult();
}

public struct TraceResult
{
    public string MethodName { get; init; }
    public string ClassName { get; init; }
    public float TimeInMilliseconds { get; init; }
    public void  
}