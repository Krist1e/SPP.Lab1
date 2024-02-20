using System.IO.Compression;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Xml;
using TracerLib;
using Console = System.Console;

Tracer tracer = new();
Person tom = new("Tom", 18, tracer);

tom.Print();
TraceResult result = tracer.GetTraceResult();
var options = new JsonSerializerOptions()
{
    WriteIndented = true
};
Console.WriteLine(JsonSerializer.Serialize(result, options));
/*string json = JsonSerializer.Serialize(result);
Console.WriteLine(json);*/


class Person
{
    private string _name;
    private int _age;
    private Tracer _tracer;
    private Building _building;
    
    public string Name => _name;
    public int Age => _age;
    
    public Person(string name, int age, Tracer tracer)
    {
        _tracer = tracer;
        _name = name;
        _age = age;
        _building = new Building(5, tracer);
    }

    public void Print()
    {
        _tracer.StartTrace();
        Console.WriteLine("Name: {0} age: {1}", _name, _age);
        Method2();
        _building.PrintNumber();
        _tracer.StopTrace();
    }

    public void Method2()
    {
        _tracer.StartTrace();
        Console.WriteLine(2);
        _tracer.StopTrace();
    }
}

public class Building
{
    private int _number;
    public int Number => _number;
    private Tracer _tracer;

    public Building(int number, Tracer tracer)
    {
        _number = number;
        _tracer = tracer;
    }

    public void PrintNumber()
    {
        _tracer.StartTrace();
        Console.WriteLine("Building #" + _number);
        _tracer.StopTrace();
    }
    
    
}