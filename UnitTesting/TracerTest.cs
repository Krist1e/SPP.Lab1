using TracerLib.Model;
using Xunit;

namespace UnitTesting;

public class TracerTest : IDisposable
{
    private const int THREAD_SLEEP_TIME = 40;
    private const int THREADS_COUNT = 5;

    private readonly Tracer _tracer = new();

    private void SingleMethod()
    {
        _tracer.StartTrace();
        Thread.Sleep(THREAD_SLEEP_TIME);
        _tracer.StopTrace();
    }

    private void MethodWithInnerMethod()
    {
        _tracer.StartTrace();
        Thread.Sleep(THREAD_SLEEP_TIME);
        SingleMethod();
        _tracer.StopTrace();
    }

    [Fact]
    public void TestSingleMethod()
    {
        SingleMethod();
        TraceResult traceResult = _tracer.GetTraceResult();

        Assert.Collection(traceResult.Threads,
            thread => Assert.Collection(thread.Methods,
                method => {
                    Assert.Equal(nameof(SingleMethod), method.MethodName);
                    Assert.Equal(nameof(TracerTest), method.ClassName);
                    Assert.Empty(method.InnerMethods);
                    Assert.True(method.TimeInMilliseconds.TotalMilliseconds >= THREAD_SLEEP_TIME);
                }
            )
        );
    }

    [Fact]
    public void TestMethodWithInnerMethod()
    {
        MethodWithInnerMethod();
        TraceResult traceResult = _tracer.GetTraceResult();

        Assert.Collection(traceResult.Threads,
            thread => Assert.Collection(thread.Methods,
                method => {
                    Assert.Equal(nameof(MethodWithInnerMethod), method.MethodName);
                    Assert.Equal(nameof(TracerTest), method.ClassName);

                    Assert.Collection(method.InnerMethods,
                        innerMethod => {
                            Assert.Equal(nameof(SingleMethod), innerMethod.MethodName);
                            Assert.Equal(nameof(TracerTest), innerMethod.ClassName);
                            Assert.Empty(innerMethod.InnerMethods);
                            Assert.True(innerMethod.TimeInMilliseconds.TotalMilliseconds >= THREAD_SLEEP_TIME);
                        }
                    );

                    Assert.True(method.TimeInMilliseconds.TotalMilliseconds >= THREAD_SLEEP_TIME * 2);
                }
            )
        );
    }

    [Fact]
    public void TestSingleMethodInMultiThreads()
    {
        var threads = new List<Thread>();
        double expectedTotalElapsedTime = 0;

        for (int i = 0; i < THREADS_COUNT; i++)
        {
            var newThread = new Thread(SingleMethod);
            threads.Add(newThread);
            newThread.Start();
            expectedTotalElapsedTime += THREAD_SLEEP_TIME;
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        TraceResult traceResult = _tracer.GetTraceResult();
        Assert.Equal(THREADS_COUNT, traceResult.Threads.Count);
        
        double totalElapsedTime = 0;
        foreach (var thread in traceResult.Threads)
        {
            Assert.Single(thread.Methods);
            Assert.Equal(nameof(SingleMethod), thread.Methods[0].MethodName);
            totalElapsedTime += thread.Methods[0].TimeInMilliseconds.TotalMilliseconds;
        }

        Assert.True(totalElapsedTime >= expectedTotalElapsedTime);
    }

    public void Dispose()
    {
        
    }
}