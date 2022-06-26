using System.Text.RegularExpressions;
using VirtuMath.Numbers;

namespace VirtuMath.Parsing;

public class Parser
{
    public Grammar ParsingGrammar
    {
        get;
    }

    public Parser()
    {
        // language=regex
        ParsingGrammar = new Grammar(new Rule[]
        {
            new Rule(@"(\+|\-)(?=(\+|\-)*\d+\.?\d*)", ParseUnaryOperator),
            new Rule(@"\d+\.?\d*", ParseLiteral)
        });
    }

    public Number Parse(string? input)
    {
        if (input is null)
            throw new ArgumentNullException(nameof(input));

        foreach (Rule rule in ParsingGrammar)
        {
            Match match = Regex.Match(input, rule.RegularExpression, rule.RegularOptions);

            if (match.Success)
                return rule.Parse.Invoke(input, match);
        }

        throw new FormatException("The given input is not in a recognized format");
    }

    private Number ParseLiteral(string input, Token match)
        => new Literal(Convert.ToDouble(input));

    private Number ParseUnaryOperator(string input, Token match)
        => new UnaryOperator(match.Value, Parse(input[match.Length..]));
}