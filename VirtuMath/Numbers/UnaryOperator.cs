namespace VirtuMath.Numbers;

public class UnaryOperator : Number
{
    private static readonly Dictionary<string, Func<Number, double>> operators =
        new Dictionary<string, Func<Number, double>>()
        {
            { "+", (n) => +n.Value },
            { "-", (n) => -n.Value }
        };

    public override double Value
        => operators[Operator].Invoke(Inside);

    public override Number[] Children
        => new Number[] { Inside };

    public readonly string Operator;
    public readonly Number Inside;

    public UnaryOperator(string op, Number inside)
    {
        Operator = op;
        Inside = inside;
    }

    public override string ToString(string? number_format, FormatEnum format)
    {
        switch (format)
        {
            case FormatEnum.Tags:
                return $"<UnaryOperator Operator={Operator} Value={Value.ToString(number_format)}>" +
                    Inside.ToString(number_format, format).Replace("\n", "\n\t");
            default:
                return Operator + Inside;
        }
    }
}
