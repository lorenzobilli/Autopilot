namespace Autopilot.MagicVariables;

public interface IMagicVariable
{
    public bool Matches(string input);

    public string GetValue();
}
