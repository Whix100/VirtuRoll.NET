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
}
