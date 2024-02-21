using TracerLib.Model;
using TracerLib.Serialization;
using TracerLib.Writer;

namespace Lab1;

internal static class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        Tracer tracer = new();
        Person tom = new("Tom", 18, tracer);
        Person bob = new("Bob", 22, tracer);
        Building building = new(5, tracer);
        
        List<Thread> threads =
        [
            new Thread(tom.Print),
            new Thread(bob.Print),
            new Thread(building.PrintNumber),
            new Thread(tom.PrintName)
        ];
        threads.ForEach(thread => thread.Start());
        threads.ForEach(thread => thread.Join());
        
        TraceResult traceResult = tracer.GetTraceResult();
        ISerializer jsonSerializer = new JsonSerializer();
        IWriter jsonWriter = new Writer(jsonSerializer);
        jsonWriter.Write(Console.OpenStandardOutput(), traceResult);
        jsonWriter.Write(new FileStream("traceResult.json", FileMode.Create), traceResult);
        
        ISerializer xmlSerializer = new XmlSerializer();
        IWriter xmlWriter = new Writer(xmlSerializer);
        xmlWriter.Write(Console.OpenStandardOutput(), traceResult);
        xmlWriter.Write(new FileStream("traceResult.xml", FileMode.Create), traceResult);
    }
}



class Person(string name, int age, Tracer tracer)
{
    private readonly Building _building = new(5, tracer);
    
    public string Name => name;
    public int Age => age;

    public void Print()
    {
        tracer.StartTrace();
        Console.WriteLine("Name: {0} age: {1}", name, age);
        PrintName();
        _building.PrintNumber();
        tracer.StopTrace();
    }

    public void PrintName()
    {
        tracer.StartTrace();
        Console.WriteLine(name);
        tracer.StopTrace();
    }
}

public class Building(int number, Tracer tracer)
{
    public void PrintNumber()
    {
        tracer.StartTrace();
        Console.WriteLine("Building #" + number);
        tracer.StopTrace();
    }
    
}