namespace VirtuMath.Numbers;

public class Literal : Number
{
    public override Number[] Children
        => Array.Empty<Number>();

    /// <summary>
    /// Creates a new Literal with the provided value.
    /// </summary>
    /// <param name="value">The value of the literal.</param>
    public Literal(double value)
        => Value = value;

    public override string ToString(string? number_format, FormatEnum format)
    {
        switch (format)
        {
            case FormatEnum.Tags:
                return ConvertToTagString<Literal>(this, number_format);
            default:
                return Value.ToString(number_format);
        }
    }
}
