namespace VirtuRoll.Numbers;

public class Dice : NumberSet
{
    public override double Value
        => Values.Select((n) => n.Value).Sum();

    public readonly int Amount;
    public readonly int Size;

    private Random random = new Random();

    public Dice(int size) : this(1, size)
    { }

    public Dice(int amount, int size) : this(amount, size, Array.Empty<Literal>())
    { }

    public Dice(int amount, int size, IEnumerable<Literal> values) : base(values)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), amount, $"Dice amount cannot be less than 1");

        if (size <= 0)
            throw new ArgumentOutOfRangeException(nameof(size), size, $"Dice size cannot be less than 1");

        Amount = amount;
        Size = size;

        Reroll();
    }

    public void AddRoll()
        => Values.Add(new Literal(random.Next(Size) + 1));

    public void Reroll()
    {
        Values.Clear();

        for (int i = 0; i < Amount; i++)
            AddRoll();
    }

    public override string ToString(string? number_format, FormatEnum format)
    {
        switch (format)
        {
            case FormatEnum.Tags:
                return ConvertToTagString<Dice>(this, number_format);
            default:
                return $"{Amount}d{Size}({String.Join(", ", Values)})";
        }
    }

    public override object Clone()
        => new Dice(Amount, Size);
}
