using System.Text.Json;
using TracerLib.Mapper;
using TracerLib.Model;

namespace TracerLib.Serialization;

public class JsonSerializer : ISerializer
{
    public string Serialize(TraceResult traceResult)
    {
        var dto = JsonDtoMapper.Map(traceResult);
        JsonSerializerOptions options = new()
        {
            WriteIndented = true
        };
        string json = System.Text.Json.JsonSerializer.Serialize(dto, options);
        return json;
    }
}