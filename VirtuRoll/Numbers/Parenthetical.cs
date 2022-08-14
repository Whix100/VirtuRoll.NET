namespace VirtuRoll.Numbers;

public class Parenthetical : Number
{
    public override double Value
        => Inside.Value;

    public override Number[] Children
        => new Number[] { Inside };

    /// <summary>
    /// The Number inside the Parenthetical.
    /// </summary>
    public readonly Number Inside;

    /// <summary>
    /// Creates a Parenthetical from the provided inside Number.
    /// </summary>
    /// <param name="inside"></param>
    public Parenthetical(Number inside)
        => Inside = inside;

    public override string ToString(string? number_format, FormatEnum format)
    {
        switch (format)
        {
            case FormatEnum.Tags:
                return ConvertToTagString<Parenthetical>(this, number_format);
            default:
                return $"({Inside.ToString(number_format, format)})";
        }
    }
}
