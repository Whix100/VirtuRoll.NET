namespace VirtuRoll.Numbers;

public class Die : NumberSet
{
    public override double Value
        => Values.Last().Value;

    public readonly int Size;
    private Random random = new Random();

    public Die(int size) : this(size, Array.Empty<Literal>())
    { }

    public Die(int size, IEnumerable<Literal> values) : base(values)
    {
        Size = size;
        AddRoll();
    }

    public void AddRoll()
    {
        // TODO.
        if (Size < 1)
            throw new Exception();

        Values.Add(new Literal(random.Next(Size) + 1));
    }

    public void ForceValue(Literal new_value)
    {
        if (Values is not null && Values.Count > 0)
            Values[^1] = new_value;
        else
            Values = new Number[] { new_value }.ToList();
    }

    public override string ToString(string? number_format, FormatEnum format)
    {
        switch (format)
        {
            case FormatEnum.Tags:
                return ConvertToTagString<Die>(this, number_format);
            default:
                return $"d{Size}";
        }
    }

    public override object Clone()
        => new Die(Size);
}
