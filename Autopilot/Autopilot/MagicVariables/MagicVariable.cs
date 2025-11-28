using System.Text.RegularExpressions;

namespace Autopilot.MagicVariables;

public abstract partial class MagicVariable : IMagicVariable
{
    protected const char DelimiterToken = '#';

    protected string _identifier;

    protected MagicVariable(string identifier)
    {
        if (!ValidIdentifierName().IsMatch(identifier))
        {
            throw new ArgumentException("Identifier must be all uppercase letters and at least two characters long.", nameof(identifier));

        }

        _identifier = identifier;
    }

    [GeneratedRegex(@"^[A-Z][A-Z]+")]
    private static partial Regex ValidIdentifierName();

    public bool Matches(string input) => Regex.IsMatch(input, $@"\{DelimiterToken}{_identifier}\{DelimiterToken}");

    public abstract string GetValue();
}
