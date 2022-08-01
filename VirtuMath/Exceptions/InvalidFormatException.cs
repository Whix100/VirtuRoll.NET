namespace VirtuMath.Exceptions;

public class InvalidFormatException : Exception
{
    /// <summary>
    /// The expression with the error.
    /// </summary>
    public readonly string? Expression;

    /// <summary>
    /// Creates an InvalidFormatException with a generic error message.
    /// </summary>
    public InvalidFormatException() : base("Unrecognized format in expression")
    { }

    /// <summary>
    /// Creates an InvalidFormatException with the expression that contains the error.
    /// </summary>
    /// <param name="expression">The expression with the error.</param>
    public InvalidFormatException(string expression) : base($"Unrecognized format in expression, '{expression}'")
        => Expression = expression;
}
