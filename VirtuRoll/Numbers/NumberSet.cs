namespace VirtuRoll.Numbers;

public class NumberSet : Number
{
    public override double Value
        => Values.Select((c) => c.Value).Sum();

    public override Number[] Children
        => Values.ToArray();

    public List<Number> Values
    {
        get;
        protected set;
    }

    public NumberSet(Number value) : this(new Number[] { value })
    { }

    public NumberSet(IEnumerable<Number> values)
        => Values = values.ToList();

    public override string ToString(string? number_format, FormatEnum format)
    {
        switch (format)
        {
            case FormatEnum.Tags:
                return ConvertToTagString<NumberSet>(this, number_format);
            default:
                return $"({String.Join(", ", Values)})";
        }
    }
}
