namespace VirtuMath.Numbers;

public class BinaryOperator : Number
{
    private static readonly Dictionary<string, Func<Number, Number, double>> operators =
        new Dictionary<string, Func<Number, Number, double>>()
        {
            { "+", (a, b) => a.Value + b.Value },
            { "-", (a, b) => a.Value - b.Value },
            { "*", (a, b) => a.Value * b.Value },
            { "/", (a, b) => a.Value / b.Value },
            { "//", (a, b) => (int)(a.Value - b.Value) },
            { "%", (a, b) => a.Value % b.Value },
            { "^", (a, b) => Math.Pow(a.Value, b.Value) }
        };

    public override double Value
        => operators[Operator].Invoke(Left, Right);

    public override Number[] Children
        => new Number[] { Left, Right };

    public readonly string Operator;
    public readonly Number Left;
    public readonly Number Right;

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
                return $"<BinaryOperator Operator={Operator} Value={Value.ToString(number_format)}>" +
                    Left.ToString(number_format, format).Replace("\n", "\n\t") + "\n" +
                    Right.ToString(number_format, format).Replace("\n", "\n\t");
            default:
                return Left + Operator + Right;
        }
    }
}
