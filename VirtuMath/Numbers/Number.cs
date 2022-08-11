namespace VirtuMath.Numbers;

public abstract class Number
{
    /// <summary>
    /// The numeric value of the Number.
    /// </summary>
    public virtual double Value
    {
        get;
        protected set;
    }

    /// <summary>
    /// The children of the Number.
    /// </summary>
    public virtual Number[] Children
    {
        get;
        protected set;
    }

    /// <summary>
    /// Creates a new Number without any children.
    /// </summary>
    public Number()
    {
        Children = Array.Empty<Number>();
    }

    /// <summary>
    /// Returns a string that represents the current Number in plain text.
    /// </summary>
    /// <returns>A string representation of the current Number.</returns>
    public override string ToString()
        => ToString(FormatEnum.PlainText);

    /// <summary>
    /// Returns a string that represents the current Number in the format speficied.
    /// </summary>
    /// <param name="format">The format to represent the Number as.</param>
    /// <returns>A string representation of the current Number.</returns>
    public virtual string ToString(FormatEnum format)
        => ToString("", format);

    /// <summary>
    /// Returns a string that represents the current Number in the format speficied.
    /// </summary>
    /// <param name="number_format">The format for the value of the Number.</param>
    /// <param name="format">The format to represent the Number as.</param>
    /// <returns>A string representation of the current Number.</returns>
    public abstract string ToString(string? number_format, FormatEnum format);

    public static implicit operator short(Number n)
        => (short)n.Value;

    public static implicit operator int(Number n)
        => (int)n.Value;

    public static implicit operator long(Number n)
        => (long)n.Value;

    public static implicit operator ushort(Number n)
        => (ushort)n.Value;

    public static implicit operator uint(Number n)
        => (uint)n.Value;

    public static implicit operator ulong(Number n)
        => (ulong)n.Value;

    public static implicit operator float(Number n)
        => (float)n.Value;

    public static implicit operator double(Number n)
        => n.Value;

    public static explicit operator Number(short n)
        => new Literal(n);

    public static explicit operator Number(int n)
        => new Literal(n);

    public static explicit operator Number(long n)
        => new Literal(n);

    public static explicit operator Number(ushort n)
        => new Literal(n);

    public static explicit operator Number(uint n)
        => new Literal(n);

    public static explicit operator Number(ulong n)
        => new Literal(n);

    public static explicit operator Number(float n)
        => new Literal(n);

    public static explicit operator Number(double n)
        => new Literal(n);

    public static bool operator ==(Number a, Number b)
        => a.Value == b.Value;

    public static bool operator !=(Number a, Number b)
        => a.Value != b.Value;

    public static bool operator >(Number a, Number b)
        => a.Value > b.Value;

    public static bool operator <(Number a, Number b)
        => a.Value < b.Value;

    public static bool operator >=(Number a, Number b)
        => a.Value >= b.Value;

    public static bool operator <=(Number a, Number b)
        => a.Value <= b.Value;
}
