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
        string ADDITION_OPERATORS = @"(\+|\-)";
        // language=regex
        string MULTIPLICATION_OPERATORS = @"(\*|/|//|%)";
        // language=regex
        string EXPONENTIAL_OPERATOR = @"(\^)";
        // language=regex
        string UNARY_OPERATORS = @"(\+|\-)";
        // language=regex
        string LITERAL = @"(\d+\.?\d*)";
        // language=regex
        string CONSTANT = @"(inf|infinity|\u221E|pi|\u03C0|e|tau|\u03C4)";
        // language=regex
        string OPERAND = $"{UNARY_OPERATORS}*({LITERAL}|{CONSTANT})";

        // language=regex
        ParsingGrammar = new Grammar(new Rule[]
        {
            new Rule($"(?<={OPERAND})({ADDITION_OPERATORS})(?={OPERAND})", ParseBinaryOperator,
                RegexOptions.IgnoreCase | RegexOptions.RightToLeft),
            new Rule($"(?<=({UNARY_OPERATORS})*({LITERAL}|{CONSTANT}))({MULTIPLICATION_OPERATORS})" +
                $"(?=({UNARY_OPERATORS})*({LITERAL}|{CONSTANT}))", ParseBinaryOperator,
                RegexOptions.IgnoreCase | RegexOptions.RightToLeft),
            new Rule($"(?<=({UNARY_OPERATORS})*({LITERAL}|{CONSTANT}))({EXPONENTIAL_OPERATOR})" +
                $"(?=({UNARY_OPERATORS})*({LITERAL}|{CONSTANT}))", ParseBinaryOperator,
                RegexOptions.IgnoreCase | RegexOptions.RightToLeft),
            new Rule($"({UNARY_OPERATORS})(?=({UNARY_OPERATORS})*({LITERAL}|{CONSTANT}))", ParseUnaryOperator),
            new Rule($"^{LITERAL}$", ParseLiteral),
            new Rule($"^{CONSTANT}$", ParseConstant)
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

    private Number ParseBinaryOperator(string input, Token match)
        => new BinaryOperator(match.Value, Parse(input[..match.Index]), Parse(input[(match.Index + match.Length)..]));

    private Number ParseUnaryOperator(string input, Token match)
        => new UnaryOperator(match.Value, Parse(input[match.Length..]));

    private Number ParseLiteral(string input, Token match)
        => new Literal(Convert.ToDouble(input));

    private Number ParseConstant(string input, Token match)
        => new Constant(input);
}