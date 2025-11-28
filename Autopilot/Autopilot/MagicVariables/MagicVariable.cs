using System.Text.RegularExpressions;

namespace Autopilot.MagicVariables;

public abstract partial class MagicVariable : IMagicVariable
{
    protected const char DelimiterToken = '#';

    public string Identifier { get; protected set; }

    protected MagicVariable(string identifier)
    {
        if (!ValidIdentifierName().IsMatch(identifier))
        {
            throw new ArgumentException("Identifier must be all uppercase letters and at least two characters long.", nameof(identifier));

        }

        Identifier = $"{DelimiterToken}{identifier}{DelimiterToken}";
    }

    [GeneratedRegex(@"^[A-Z][A-Z]+")]
    private static partial Regex ValidIdentifierName();

    public bool Matches(string input) => Regex.IsMatch(input, $@"\{Identifier}");

    public abstract string GetValue();
}
