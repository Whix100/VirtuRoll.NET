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

    /// <summary>
    /// The operator to set the function of the UnaryOperator.
    /// </summary>
    public readonly string Operator;

    /// <summary>
    /// The Number inside the UnaryOperator.
    /// </summary>
    public readonly Number Inside;

    /// <summary>
    /// Creates a UnaryOperator from the specified operator and inside Number.
    /// </summary>
    /// <param name="op">The operator for the UnaryOperator.</param>
    /// <param name="inside">The inside Number for the UnaryOperator.</param>
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
