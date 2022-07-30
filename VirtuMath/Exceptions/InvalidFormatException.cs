namespace VirtuMath.Exceptions;

public class InvalidFormatException : Exception
{
    public readonly string? Expression;

    public InvalidFormatException() : base("Unrecognized format in expression")
    { }

    public InvalidFormatException(string expression) : base($"Unrecognized format in expression, '{expression}'")
        => Expression = expression;
}
