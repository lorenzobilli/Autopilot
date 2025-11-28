using Autopilot.MagicVariables;
using Autopilot.Model;
using System.Text.Json;

namespace Autopilot;

public class DirectiveHandler
{
    private readonly IList<Directive> _directives;

    private readonly IList<IMagicVariable> _magicVariables;

    public DirectiveHandler(string file)
    {
        if (!file.EndsWith(".json"))
        {
            throw new ArgumentException("Directive file must be a JSON file.", nameof(file));
        }
        if (!File.Exists(file))
        {
            throw new ArgumentException("Directive file does not exist.", nameof(file));
        }

        var json = File.ReadAllText(file);
        _directives = JsonSerializer.Deserialize(json, DirectiveSourceGenerationContext.Default.IListDirective)
            ?? throw new InvalidOperationException("Failed to deserialize directives from file.");

        _magicVariables =
        [
            new Home()
        ];
    }

    public void ParseMagicVariables()
    {
        foreach (var directive in _directives)
        {
            foreach (var magicVariable in _magicVariables)
            {
                if (magicVariable.Matches(directive.Location))
                {
                    var value = magicVariable.GetValue();
                    directive.Location = directive.Location.Replace($"{magicVariable.Identifier}", value);
                }
            }
        }
    }
}
