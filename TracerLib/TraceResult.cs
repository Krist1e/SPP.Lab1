namespace TracerLib;

public struct TraceResult
{
    public string MethodName { get; set; }
    public string ClassName { get; set; }
    public float TimeInMilliseconds { get; set; }
}