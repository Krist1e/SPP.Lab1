using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TracerLib.Model;

[DataContract]
public class ThreadTraceResult(int threadId)
{
    private TimeSpan _totalTimeInMilliseconds;
    private readonly List<MethodTraceResult> _methods = new();
    private readonly Stack<MethodTraceResult> _callStack = new();
    public int ThreadId => threadId;
    public TimeSpan TotalTimeInMilliseconds => _totalTimeInMilliseconds;
    public List<MethodTraceResult> Methods => _methods;

    public void StartThreadTrace(MethodTraceResult method)
    {
        if (_callStack.Count == 0)
        {
            _methods.Add(method);
        }
        else
        {
            _callStack.Peek().AddInnerMethod(method);
        }
        _callStack.Push(method);
        method.StartMethodTrace();
    }

    public void StopThreadTrace()
    {
        MethodTraceResult method = _callStack.Peek();
        method.StopMethodTrace();
        if (_callStack.Count == 1)
        {
            _totalTimeInMilliseconds += method.TimeInMilliseconds;
        }
        _callStack.Pop();
    }
}
