namespace VirtuRoll.Numbers;

public class Constant : Number
{
    private static readonly Dictionary<string, double> symbols
        = new Dictionary<string, double>()
        {
            { "inf", Double.PositiveInfinity },
            { "infinity", Double.PositiveInfinity },
            { "\u221E", Double.PositiveInfinity },
            { "pi", Math.PI },
            { "\u03C0", Math.PI },
            { "e", Math.E },
            { "tau", Math.Tau },
            { "\u03C4", Math.Tau }
        };

    public override double Value
        => symbols[Symbol];

    public override Number[] Children
        => Array.Empty<Number>();

    /// <summary>
    /// The symbol that represents the Constant.
    /// </summary>
    public readonly string Symbol;

    /// <summary>
    /// Creates a constant using the provided symbol.
    /// </summary>
    /// <param name="symbol">The symbol for the value of the Constant.</param>
    public Constant(string symbol)
        => Symbol = symbol;

    public override string ToString(string? number_format, FormatEnum format)
    {
        switch (format)
        {
            case FormatEnum.Tags:
                return ConvertToTagString<Constant>(this, number_format);
            default:
                return Symbol;
        }
    }

    public override object Clone()
        => new Constant(Symbol);
}
