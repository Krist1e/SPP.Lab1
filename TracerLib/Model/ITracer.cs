namespace TracerLib.Model;

public interface ITracer<out T>
{
    void StartTrace();
    void StopTrace();
    T GetTraceResult();
}