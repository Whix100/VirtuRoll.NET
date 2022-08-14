namespace VirtuRoll.Exceptions;

public class UnbalancedParenthesesException : Exception
{
    /// <summary>
    /// The expression with the error.
    /// </summary>
    public readonly string? Expression;

    /// <summary>
    /// Creates an UnbalancedParenthesesException with a generic error message.
    /// </summary>
    public UnbalancedParenthesesException() : base("Unbalanced parentheses in expression")
    { }

    /// <summary>
    /// Creates an UnbalancedParenthesesException with the expression that contains the error.
    /// </summary>
    /// <param name="expression">The expression with the error.</param>
    public UnbalancedParenthesesException(string expression)
        : base($"Unbalanced parentheses in expression, '{expression}'")
        => Expression = expression;
}
