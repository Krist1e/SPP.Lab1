using System.Text.Json.Serialization;

namespace TracerLib.Dto.Json;

public class JsonTraceResultDto
{
    [JsonPropertyName("threads")]
    public List<JsonThreadTraceResultDto> Threads { get; set; }
}