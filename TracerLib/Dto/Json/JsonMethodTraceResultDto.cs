using System.Text.Json.Serialization;

namespace TracerLib.Dto.Json;

public class JsonMethodTraceResultDto
{
    [JsonPropertyName("name")] 
    public string Name { get; set; }
    [JsonPropertyName("class")]
    public string Class { get; set; }
    [JsonPropertyName("time")]
    public string Time { get; set; }
    [JsonPropertyName("methods")]
    public List<JsonMethodTraceResultDto> Methods { get; set; }
}