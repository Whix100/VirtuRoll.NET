namespace VirtuMath.Exceptions;

public class InvalidFormatException : Exception
{
    public InvalidFormatException() : base("The given input is not in a recognized format.")
    { }

    public InvalidFormatException(string input) : base($"The given input, '{input}', is not in a recognized format.")
    { }
}
