using Autopilot.MagicVariables;
using Autopilot.Model;
using System.Text.Json;

namespace Autopilot;

public class DirectiveHandler
{
    private readonly IList<IMagicVariable> _magicVariables;

    public string ExecutionEnvironment { get; private set; }

    public IList<Directive> Directives { get; private set; }

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
        var directiveList = JsonSerializer.Deserialize(json, DirectiveListSourceGenerationContext.Default.DirectiveList)
            ?? throw new InvalidOperationException("Failed to deserialize directives from file.");

        ExecutionEnvironment = directiveList.ExecutionEnvironment;
        Directives = directiveList.Directives;

        _magicVariables =
        [
            new Home()
        ];
    }

    public void ParseMagicVariables()
    {
        foreach (var directive in Directives)
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
