namespace Autopilot.MagicVariables;

public class Home : MagicVariable, IMagicVariable
{
    public Home() : base("HOME")
    {
    }

    public override string GetValue() => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
}
