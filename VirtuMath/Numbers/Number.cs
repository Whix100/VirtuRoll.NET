namespace VirtuMath.Numbers;

public abstract class Number
{
    public virtual double Value
    {
        get;
        protected set;
    }

    public virtual Number[] Children
    {
        get;
        protected set;
    }

    public Number()
    {
        Children = Array.Empty<Number>();
    }

    public override string ToString()
        => ToString(FormatEnum.PlainText);

    public virtual string ToString(FormatEnum format)
        => ToString("", format);

    public abstract string ToString(string? number_format, FormatEnum format);
}
