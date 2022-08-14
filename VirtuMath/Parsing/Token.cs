using System.Text.RegularExpressions;

namespace VirtuMath.Parsing;

public struct Token
{
    /// <summary>
    /// The value of a Rule match.
    /// </summary>
    public readonly string Value;
    /// <summary>
    /// The index of the parent string the match was found in.
    /// </summary>
    public readonly int Index;

    public int Length
        => Value.Length;

    /// <summary>
    /// Creates a Token parsing rule match.
    /// </summary>
    /// <param name="value">The value of the rule match.</param>
    /// <param name="index">The index of the parent string the match was found in.</param>
    public Token(string value, int index)
    {
        Value = value;
        Index = index;
    }

    /// <summary>
    /// Converts a regex Match to a Token.
    /// </summary>
    /// <param name="match">The regex Match to be converted.</param>
    public static implicit operator Token(Match match)
        => new Token(match.Value, match.Index);
}
