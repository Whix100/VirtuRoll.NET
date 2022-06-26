using System.Text.RegularExpressions;
using VirtuMath.Numbers;

namespace VirtuMath.Parsing;

public struct Rule
{
    public readonly string RegularExpression;
    public readonly Func<string, Token, Number> Parse;
    public readonly RegexOptions RegularOptions;

    public Rule(string regex, Func<string, Token, Number> parse, RegexOptions regex_options = RegexOptions.IgnoreCase)
    {
        RegularExpression = regex;
        Parse = parse;
        RegularOptions = regex_options;
    }
}
