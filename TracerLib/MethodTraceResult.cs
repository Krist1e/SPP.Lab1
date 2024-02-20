using System.Diagnostics;
using System.Reflection;

namespace TracerLib;

public class MethodTraceResult(MethodBase method)
{
    private Stopwatch _stopwatch = new();
    private string _methodName = method.Name;
    private string _className = method.ReflectedType!.Name;
    private float _timeInMilliseconds;
    private IList<MethodTraceResult> _innerMethods = new List<MethodTraceResult>();
    
    public string MethodName => _methodName;
    public string ClassName => _className;
    public float TimeInMilliseconds => _timeInMilliseconds;
    public IList<MethodTraceResult> InnerMethods => _innerMethods;

    public void StartMethodTrace()
    {
        _stopwatch.Start();
    }

    public void StopMethodTrace()
    {
        _stopwatch.Stop();
        _timeInMilliseconds = _stopwatch.ElapsedMilliseconds;
    }
    
    public void AddInnerMethod(MethodTraceResult methodTraceResult) => _innerMethods.Add(methodTraceResult);
}