namespace VirtuRoll.Numbers;

public class BinaryOperator : Number
{
    private static readonly Dictionary<string, Func<Number, Number, double>> operators =
        new Dictionary<string, Func<Number, Number, double>>()
        {
            { "+", (a, b) => a.Value + b.Value },
            { "-", (a, b) => a.Value - b.Value },
            { "*", (a, b) => a.Value * b.Value },
            { "/", (a, b) => a.Value / b.Value },
            { "//", (a, b) => (int)Math.Round(a.Value / b.Value) },
            { "%", (a, b) => a.Value % b.Value },
            { "^", (a, b) => Math.Pow(a.Value, b.Value) }
        };

    public override double Value
        => operators[Operator].Invoke(Left, Right);

    public override Number[] Children
        => new Number[] { Left, Right };

    /// <summary>
    /// The operator to set the function of the BinaryOperator.
    /// </summary>
    public readonly string Operator;

    /// <summary>
    /// The left operand of the BinaryOperator.
    /// </summary>
    public readonly Number Left;

    /// <summary>
    /// The right operand of the BinaryOperator.
    /// </summary>
    public readonly Number Right;

    /// <summary>
    /// Creates a BinaryOperator from the operator, left operand, and right operand.
    /// </summary>
    /// <param name="op">The operator for the BinaryOperator.</param>
    /// <param name="left">The left operand for the BinaryOperator.</param>
    /// <param name="right">The righr operand for the BinaryOperator.</param>
    public BinaryOperator(string op, Number left, Number right)
    {
        Operator = op;
        Left = left;
        Right = right;
    }

    public override string ToString(string? number_format, FormatEnum format)
    {
        switch (format)
        {
            case FormatEnum.Tags:
                return ConvertToTagString<BinaryOperator>(this, number_format);
            default:
                return Left + Operator + Right;
        }
    }
}
