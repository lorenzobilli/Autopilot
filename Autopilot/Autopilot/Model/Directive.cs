using System.Text.Json.Serialization;

namespace Autopilot.Model;

public class Directive
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("file")]
    public required string File { get; init; }

    [JsonPropertyName("location")]
    public required string Location { get; set; }

    [JsonPropertyName("environment")]
    public string? Environment { get; init; }
}

[JsonSerializable(typeof(Directive))]
internal partial class DirectiveSourceGenerationContext : JsonSerializerContext { }