using System.Reflection;

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

    /// <summary>
    /// Converts a Number to the tag format.
    /// </summary>
    /// <typeparam name="T">An object type that inherits from Number.</typeparam>
    /// <param name="number">The Number to convert to a string.</param>
    /// <param name="number_format">The format to use for numeric values.</param>
    /// <returns>The string representation of the given number in the tag format.</returns>
    protected static string ConvertToTagString<T>(T number, string? number_format)
        where T : Number
    {
        IEnumerable<string> members = number.GetType().GetProperties()
            .Where((p) => !p.Name.Equals("Children") && p.PropertyType != typeof(Number))
            .Select((p) => $"{p.Name} = {ConvertToTypeString(p.GetValue(number), number_format)}")
            .Concat(
                number.GetType().GetFields()
                .Where((f) => f.FieldType != typeof(Number))
                .Select((f) => $"{f.Name} = {ConvertToTypeString(f.GetValue(number), number_format)}"));

        return $"<{number.GetType().Name} {String.Join(" ", members)}>" +
            String.Join("", number.Children.Select(
                (n) => $"\n\t{n.ToString(number_format, FormatEnum.Tags).Replace("\n", "\n\t")}"));
    }

    /// <summary>
    /// Returns the string representation of an object with decoratives.
    /// </summary>
    /// <param name="obj">The object to convert.</param>
    /// <param name="number_format">The format to use for numeric values.</param>
    /// <returns>The string representation of the object with decoratives.</returns>
    private static string? ConvertToTypeString(object? obj, string? number_format = null)
    {
        if (obj is string)
            return $"\"{obj}\"";
        else if (obj is char)
            return $"'{obj}'";
        else if (obj is double)
            return ((double)obj).ToString(number_format);
        else
            return obj?.ToString();
    }
}
