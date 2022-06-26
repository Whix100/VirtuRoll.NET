namespace VirtuMath.Numbers;

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

    public readonly string Symbol;

    public Constant(string symbol)
        => Symbol = symbol;

    public override string ToString(string? number_format, FormatEnum format)
    {
        switch (format)
        {
            case FormatEnum.Tags:
                return $"<Constant Symbol={Symbol}>";
            default:
                return Symbol;
        }
    }
}
