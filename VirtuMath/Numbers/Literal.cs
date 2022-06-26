namespace VirtuMath.Numbers;

public class Literal : Number
{
    public override Number[] Children
        => Array.Empty<Number>();

    public Literal(double value)
        => Value = value;

    public override string ToString(string? number_format, FormatEnum format)
    {
        switch (format)
        {
            case FormatEnum.Tags:
                return $"<Literal Value={Value.ToString(number_format)}>";
            default:
                return Value.ToString(number_format);
        }
    }
}
