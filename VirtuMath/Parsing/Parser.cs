using System.Text.RegularExpressions;
using VirtuMath.Exceptions;
using VirtuMath.Numbers;

namespace VirtuMath.Parsing;

public class Parser
{
    /// <summary>
    /// The ParsingGrammar the Parser will use.
    /// </summary>
    public Grammar ParsingGrammar
    {
        get;
    }

    /// <summary>
    /// Creates a Parser with the default Grammar in it.
    /// </summary>
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
        string OPERAND = $@"\s*({UNARY_OPERATORS}\s*)*({LITERAL}|{CONSTANT}|\[\d+\])\s*";

        // language=regex
        ParsingGrammar = new Grammar(new Rule[]
        {
            new Rule($"(?<={OPERAND})({ADDITION_OPERATORS})(?={OPERAND})", ParseBinaryOperator,
                RegexOptions.IgnoreCase | RegexOptions.RightToLeft),
            new Rule($"(?<={OPERAND})({MULTIPLICATION_OPERATORS})(?={OPERAND})", ParseBinaryOperator,
                RegexOptions.IgnoreCase | RegexOptions.RightToLeft),
            new Rule($"(?<={OPERAND})({EXPONENTIAL_OPERATOR})(?={OPERAND})", ParseBinaryOperator,
                RegexOptions.IgnoreCase | RegexOptions.RightToLeft),
            new Rule($"({UNARY_OPERATORS})(?={OPERAND})", ParseUnaryOperator),
            new Rule($@"(?<=^\s*){LITERAL}(?=\s*$)", ParseLiteral),
            new Rule($@"(?<=^\s*){CONSTANT}(?=\s*$)", ParseConstant)
        });
    }

    /// <summary>
    /// Parses an expression into a Number.
    /// </summary>
    /// <param name="input">A math expression to be parsed.</param>
    /// <returns>A Number created from the inputted expression.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <c>input</c> is <c>null</c>.</exception>
    public Number Parse(string input)
    {
        if (input is null)
            throw new ArgumentNullException(nameof(input));

        if (input.Contains('(') || input.Contains(')'))
            return ParseWithParentheses(input);
        else
            return ParseWithoutParentheses(input);
    }

    /// <summary>
    /// Parses an expression that does not contain parentheses.
    /// </summary>
    /// <param name="input">A math expression without parentheses to be parsed.</param>
    /// <returns>A Number created from the inputted expression.</returns>
    /// <exception cref="InvalidFormatException">
    /// Thrown if <c>input</c> doesn't match any of the rules in <c>ParsingGrammar</c>
    /// </exception>
    protected Number ParseWithoutParentheses(string input)
    {
        foreach (Rule rule in ParsingGrammar)
        {
            Match match = Regex.Match(input, rule.RegularExpression, rule.RegularOptions);

            if (match.Success)
                return rule.Parse.Invoke(input, match);
        }

        throw new InvalidFormatException(input);
    }

    /// <summary>
    /// Parses an expression that contains parentheses.
    /// </summary>
    /// <param name="input">A math expression with parentheses to be parsed.</param>
    /// <returns>A Number created from the inputted expression.</returns>
    /// <exception cref="InvalidFormatException">
    /// Thrown if <c>input</c> doesn't match any of the rules in <c>ParsingGrammar</c>
    /// </exception>
    protected Number ParseWithParentheses(string input)
    {
        string tok_input = TokenizeInput(input, out List<Token> tokens);

        if (Regex.IsMatch(tok_input, @"^\[\d+\]$"))
            return new Parenthetical(Parse(input[1..^1]));

        foreach (Rule rule in ParsingGrammar)
        {
            Match match = Regex.Match(tok_input, rule.RegularExpression, rule.RegularOptions);

            if (match.Success)
                return rule.Parse.Invoke(input,
                    new Token(match.Value, DetokenizeIndex(tok_input, tokens, match.Index)));
        }

        throw new InvalidFormatException(input);
    }

    /// <summary>
    /// Replaces parenthetical parts of the expression with easier to parse token tags.
    /// </summary>
    /// <param name="input">The expression to be tokenized.</param>
    /// <param name="tokens">A List of Tokens to be outputted.</param>
    /// <returns>A string with parentheticals replaced with token tags.</returns>
    /// <exception cref="UnbalancedParenthesesException"></exception>
    protected static string TokenizeInput(string input, out List<Token> tokens)
    {
        tokens = new List<Token>();

        string reference = String.Empty;
        string clean_input = Regex.Replace(input, @"[\\\[\]]", new MatchEvaluator((m) => $@"\{m}"));
        int start = -1;
        int count = -1;

        for (int i = 0; i < clean_input.Length; i++)
        {
            if (clean_input[i] == '(')
            {
                if (i == clean_input.Length - 1)
                {
                    throw new UnbalancedParenthesesException(input);
                }
                else if (start == -1)
                {
                    start = i;
                    count++;
                }
                else
                {
                    count++;
                }
            }
            else if (clean_input[i] == ')')
            {
                if (start == -1)
                {
                    throw new UnbalancedParenthesesException(input);
                }
                else if (count > 0)
                {
                    count--;
                }
                else
                {
                    tokens.Add(new Token(clean_input[start..(i + 1)], start));
                    start = -1;
                    count = -1;
                    reference += $"[{tokens.Count - 1}]";
                }
            }
            else if (count == -1)
            {
                reference += clean_input[i];
            }
        }

        input = reference;
        return reference;
    }

    /// <summary>
    /// Converts a tokenized expression into a non-tokenized expression while getting the index offset of a match in the
    /// expression.
    /// </summary>
    /// <param name="input">The tokenized expression.</param>
    /// <param name="tokens">The tokens that belong in the expression.</param>
    /// <param name="index">The index of the position of the match.</param>
    protected static int DetokenizeIndex(string input, List<Token> tokens, int index)
    {
        int offset = 0;
        MatchCollection matches = Regex.Matches(input, @"\[\d+\]");

        for (int i = 0; i < matches.Count; i++)
            if (matches[i].Index + offset < index)
                offset += tokens[i].Length - matches[i].Length;
            else
                break;

        return index + offset;
    }

    /// <summary>
    /// Parses an expression into a BinaryOperator.
    /// </summary>
    /// <param name="input">A math expression to be parsed.</param>
    /// <param name="match">The regex match for the parsing.</param>
    /// <returns>A BinaryOperator parsed from the input expression.</returns>
    private Number ParseBinaryOperator(string input, Token match)
        => new BinaryOperator(match.Value, Parse(input[..match.Index]), Parse(input[(match.Index + match.Length)..]));

    /// <summary>
    /// Parses an expression into a UnaryOperator.
    /// </summary>
    /// <param name="input">A math expression to be parsed.</param>
    /// <param name="match">The regex match for the parsing.</param>
    /// <returns>A UnaryOperator parsed from the input expression.</returns>
    private Number ParseUnaryOperator(string input, Token match)
        => new UnaryOperator(match.Value, Parse(input[match.Length..]));

    /// <summary>
    /// Parses an expression into a Literal.
    /// </summary>
    /// <param name="input">A math expression to be parsed.</param>
    /// <param name="match">The regex match for the parsing.</param>
    /// <returns>A Literal parsed from the input expression.</returns>
    private Number ParseLiteral(string input, Token match)
        => new Literal(Convert.ToDouble(match.Value));

    /// <summary>
    /// Parses an expression into a Constant.
    /// </summary>
    /// <param name="input">A math expression to be parsed.</param>
    /// <param name="match">The regex match for the parsing.</param>
    /// <returns>A Constant parsed from the input expression.</returns>
    private Number ParseConstant(string input, Token match)
        => new Constant(match.Value);
}