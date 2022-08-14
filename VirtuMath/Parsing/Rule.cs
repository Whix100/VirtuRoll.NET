using System.Text.RegularExpressions;
using VirtuMath.Numbers;

namespace VirtuMath.Parsing;

public struct Rule
{
    /// <summary>
    /// The regex to use to match.
    /// </summary>
    public readonly string RegularExpression;
    /// <summary>
    /// The function to use when parsing the expression to a Number.
    /// </summary>
    public readonly Func<string, Token, Number> Parse;
    /// <summary>
    /// The options for RegularExpression to use when finding a match.
    /// </summary>
    public readonly RegexOptions RegularOptions;

    /// <summary>
    /// Create a Rule to use to parse an expression into a Number.
    /// </summary>
    /// <param name="regex">The regex to use to find a match to parse.</param>
    /// <param name="parse">The function to use to parse an expression to a Number.</param>
    /// <param name="regex_options">The options for finding the regex match.</param>
    public Rule(string regex, Func<string, Token, Number> parse, RegexOptions regex_options = RegexOptions.IgnoreCase)
    {
        RegularExpression = regex;
        Parse = parse;
        RegularOptions = regex_options;
    }
}
