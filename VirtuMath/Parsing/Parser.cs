using System.Text.RegularExpressions;
using VirtuMath.Exceptions;
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

    public Number Parse(string input)
    {
        if (input is null)
            throw new ArgumentNullException(nameof(input));

        if (input.Contains('(') || input.Contains(')'))
            return ParseWithParentheses(input);
        else
            return ParseWithoutParentheses(input);
    }

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

    protected Number ParseWithParentheses(string input)
    {
        string tok_input = TokenizeInput(input, out List<Token> tokens);

        if (Regex.IsMatch(tok_input, @"^\[\d+\]$"))
            return new Parenthetical(Parse(input[1..^1]));

        foreach (Rule rule in ParsingGrammar)
        {
            Match match = Regex.Match(tok_input, rule.RegularExpression, rule.RegularOptions);

            if (match.Success)
            {
                int index = match.Index;

                DetokenizeInput(tok_input, tokens, ref index);
                return rule.Parse.Invoke(input, new Token(match.Value, index));
            }
        }

        throw new InvalidFormatException(input);
    }

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
                    throw new Exception("Parenthesis are unbalanced");
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
                    throw new Exception("Parenthesis are unbalanced");
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

    protected static string DetokenizeInput(string input, List<Token> tokens, ref int index)
    {
        string output = input;
        int offset = 0;
        MatchCollection matches = Regex.Matches(input, @"\[\d+\]");

        for (int i = 0; i < matches.Count; i++)
        {
            if (matches[i].Index + offset < index)
                offset += tokens[i].Length - matches[i].Length;

            output = output.Replace($"[{i}]", tokens[i].Value);
        }

        index += offset;
        return output;
    }

    private Number ParseBinaryOperator(string input, Token match)
        => new BinaryOperator(match.Value, Parse(input[..match.Index]), Parse(input[(match.Index + match.Length)..]));

    private Number ParseUnaryOperator(string input, Token match)
        => new UnaryOperator(match.Value, Parse(input[match.Length..]));

    private Number ParseLiteral(string input, Token match)
        => new Literal(Convert.ToDouble(match.Value));

    private Number ParseConstant(string input, Token match)
        => new Constant(match.Value);
}