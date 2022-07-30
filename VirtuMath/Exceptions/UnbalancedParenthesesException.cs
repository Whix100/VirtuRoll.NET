namespace VirtuMath.Exceptions;

public class UnbalancedParenthesesException : Exception
{
    public readonly string? Expression;

    public UnbalancedParenthesesException() : base("Unbalanced parentheses in expression")
    { }

    public UnbalancedParenthesesException(string expression)
        : base($"Unbalanced parentheses in expression, '{expression}'")
        => Expression = expression;
}
