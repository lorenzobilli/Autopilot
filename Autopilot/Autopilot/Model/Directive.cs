using System.Text.Json.Serialization;

namespace Autopilot.Model;

public class Directive
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("group")]
    public required string Group { get; init; }

    [JsonPropertyName("file")]
    public required string File { get; init; }

    [JsonPropertyName("location")]
    public required string Location { get; set; }

    [JsonPropertyName("environment")]
    public string? Environment { get; init; }
}

[JsonSerializable(typeof(Directive))]
[JsonSerializable(typeof(IList<Directive>))]
internal partial class DirectiveSourceGenerationContext : JsonSerializerContext { }