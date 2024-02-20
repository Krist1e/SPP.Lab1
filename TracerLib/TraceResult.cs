using System.Collections.Concurrent;

namespace TracerLib;

public struct TraceResult
{
    private ConcurrentDictionary<int, ThreadTraceResult> _threads;
    public TraceResult()
    {
        _threads = new ConcurrentDictionary<int, ThreadTraceResult>();
    }

    public void StartTrace(int threadId, MethodTraceResult method)
    {
        var thread = _threads.GetOrAdd(threadId, new ThreadTraceResult(threadId));
        thread.StartThreadTrace(method);
    }

    public void StopTrace(int threadId)
    {
        if (_threads.TryGetValue(threadId, out var thread))
            thread.StopThreadTrace();
    }
    
}