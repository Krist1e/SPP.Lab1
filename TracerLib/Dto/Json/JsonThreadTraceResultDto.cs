using System.Text.Json.Serialization;

namespace TracerLib.Dto.Json;

public class JsonThreadTraceResultDto
{
    [JsonPropertyName("id")]
    public int ThreadId { get; set; }
    [JsonPropertyName("time")]
    public string Time { get; set; }
    [JsonPropertyName("methods")]
    public List<JsonMethodTraceResultDto> Methods { get; set; }
}