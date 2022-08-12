namespace VirtuMath.Numbers;

public abstract class Number : IComparable, IComparable<Number>, IConvertible, IEquatable<Number>
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

    public override int GetHashCode()
        => Value.GetHashCode();

    public override bool Equals(object? obj)
    {
        if (obj is Number n)
            return Equals(n);

        return false;
    }

    public bool Equals(Number? n)
    {
        if (n is null)
            return false;

        return n.Value == Value;
    }

    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is Number n)
            return CompareTo(n);

        throw new ArgumentException("Object must be of type Number.");
    }

    public int CompareTo(Number? n)
    {
        if (n is null)
            return 1;

        if (this < n)
            return -1;

        if (this > n)
            return 1;

        return 0;
    }

    public TypeCode GetTypeCode()
        => TypeCode.Object;

    public bool ToBoolean(IFormatProvider? provider)
        => Convert.ToBoolean(Value);

    public byte ToByte(IFormatProvider? provider)
        => Convert.ToByte(Value);

    public char ToChar(IFormatProvider? provider)
        => throw new InvalidCastException($"Invalid cast from '{GetType()}' to 'Char'");

    public DateTime ToDateTime(IFormatProvider? provider)
        => throw new InvalidCastException($"Invalid cast from '{GetType()}' to 'DateTime'");

    public decimal ToDecimal(IFormatProvider? provider)
        => Convert.ToDecimal(Value);

    public double ToDouble(IFormatProvider? provider)
        => Value;

    public short ToInt16(IFormatProvider? provider)
        => Convert.ToInt16(Value);

    public int ToInt32(IFormatProvider? provider)
        => Convert.ToInt32(Value);

    public long ToInt64(IFormatProvider? provider)
        => Convert.ToInt64(Value);

    public sbyte ToSByte(IFormatProvider? provider)
        => Convert.ToSByte(Value);

    public float ToSingle(IFormatProvider? provider)
        => Convert.ToSingle(Value);

    public string ToString(IFormatProvider? provider)
        => Value.ToString(provider);

    public object ToType(Type conversion_type, IFormatProvider? provider)
        => Convert.ChangeType(Value, conversion_type);

    public ushort ToUInt16(IFormatProvider? provider)
        => Convert.ToUInt16(Value);

    public uint ToUInt32(IFormatProvider? provider)
        => Convert.ToUInt32(Value);

    public ulong ToUInt64(IFormatProvider? provider)
        => Convert.ToUInt64(Value);

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

    public static Number operator -(Number a)
        => new UnaryOperator("-", a);

    public static Number operator +(Number a)
        => new UnaryOperator("+", a);

    public static Number operator +(Number a, Number b)
        => new BinaryOperator("+", a, b);

    public static Number operator -(Number a, Number b)
        => new BinaryOperator("-", a, b);

    public static Number operator *(Number a, Number b)
        => new BinaryOperator("*", a, b);

    public static Number operator /(Number a, Number b)
        => new BinaryOperator("/", a, b);

    public static Number operator %(Number a, Number b)
        => new BinaryOperator("%", a, b);
}
