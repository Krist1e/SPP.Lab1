using System.Collections.Immutable;
using System.Reflection;

namespace TracerLib;

public class ThreadTraceResult(int threadId)
{
    private  int _threadId = threadId;
    private float _totalTimeInMilliseconds;
    private List<MethodTraceResult> _methods = new();
    private Stack<MethodTraceResult> _callStack = new();

    public int ThreadId => _threadId;
    public float TotalTimeInMilliseconds => _totalTimeInMilliseconds;
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
            _totalTimeInMilliseconds = method.TimeInMilliseconds;
        }
        _callStack.Pop();
    }
}
