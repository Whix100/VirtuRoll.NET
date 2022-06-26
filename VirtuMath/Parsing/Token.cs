using System.Text.RegularExpressions;

namespace VirtuMath.Parsing;

public struct Token
{
    public readonly string Value;
    public readonly int Index;

    public int Length
        => Value.Length;

    public Token(string value, int index)
    {
        Value = value;
        Index = index;
    }

    public static implicit operator Token(Match match)
        => new Token(match.Value, match.Index);
}
