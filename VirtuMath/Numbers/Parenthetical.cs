namespace VirtuMath.Numbers;

public class Parenthetical : Number
{
    public override double Value
        => Inside.Value;

    public override Number[] Children
        => new Number[] { Inside };

    public readonly Number Inside;

    public Parenthetical(Number inside)
        => Inside = inside;

    public override string ToString(string? number_format, FormatEnum format)
    {
        switch (format)
        {
            case FormatEnum.Tags:
                return $"<Constant>" +
                    Inside.ToString(number_format, format).Replace("\n", "\n\t");
            default:
                return $"({Inside.ToString(number_format, format)})";
        }
    }
}
