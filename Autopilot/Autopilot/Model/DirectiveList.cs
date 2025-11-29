using System.Text.Json.Serialization;

namespace Autopilot.Model;

public class DirectiveList
{
    [JsonPropertyName("execution_environment")]
    public string? ExecutionEnvironment { get; set; }

    [JsonPropertyName("directives")]
    public required IList<Directive> Directives { get; init; }
}

[JsonSerializable(typeof(DirectiveList))]
internal partial class DirectiveListSourceGenerationContext : JsonSerializerContext { }