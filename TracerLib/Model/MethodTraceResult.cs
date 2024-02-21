using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TracerLib.Model;

[DataContract]
public class MethodTraceResult(MethodBase method)
{
    private readonly Stopwatch _stopwatch = new();
    private TimeSpan _timeInMilliseconds;
    private readonly List<MethodTraceResult> _innerMethods = new();
    
    public string MethodName { get; private set; } = method.Name;
    public string ClassName { get; private set; } = method.ReflectedType!.Name;
    public TimeSpan TimeInMilliseconds => _timeInMilliseconds;
    public List<MethodTraceResult> InnerMethods => _innerMethods;

    public void StartMethodTrace()
    {
        _stopwatch.Start();
    }

    public void StopMethodTrace()
    {
        _stopwatch.Stop();
        _timeInMilliseconds = _stopwatch.Elapsed;
    }
    
    public void AddInnerMethod(MethodTraceResult methodTraceResult) => _innerMethods.Add(methodTraceResult);
}