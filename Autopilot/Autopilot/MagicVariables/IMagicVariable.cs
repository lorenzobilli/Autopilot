namespace Autopilot.MagicVariables;

public interface IMagicVariable
{
    public string Identifier { get; }

    public bool Matches(string input);

    public string GetValue();
}
