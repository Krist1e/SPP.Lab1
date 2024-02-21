using System.Collections.Concurrent;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace TracerLib.Model;

[DataContract]
public struct TraceResult()
{
    private readonly ConcurrentDictionary<int, ThreadTraceResult> _threads = new();
    public IReadOnlyList<ThreadTraceResult> Threads { get; private set; }

    public void StartTrace(int threadId, MethodTraceResult method)
    {
        var thread = _threads.GetOrAdd(threadId, new ThreadTraceResult(threadId));
        thread.StartThreadTrace(method);
    }

    public void StopTrace(int threadId) 
    {
        if (_threads.TryGetValue(threadId, out var thread))
            thread.StopThreadTrace();
        Threads = _threads.Values.ToList();
    }
    
}