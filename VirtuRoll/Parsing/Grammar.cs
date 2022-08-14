namespace VirtuRoll.Parsing;

public class Grammar : List<Rule>
{
    /// <summary>
    /// Creates a new empty Grammar for parsing.
    /// </summary>
    public Grammar() : base() { }

    /// <summary>
    /// Creates a new Grammar for parsing based on a IEnumerable\<Rule\>.
    /// </summary>
    /// <param name="rules">The enumerable of Rules to create the Grammar from.</param>
    public Grammar(IEnumerable<Rule> rules) : base(rules) { }

    /// <summary>
    /// Change the priority of a Rule in the Grammar.
    /// </summary>
    /// <param name="old_priority">The old index of the Rule to change priority of.</param>
    /// <param name="new_priority">The new index of the Rule to change priority of.</param>
    public void ChangePriority(int old_priority, int new_priority)
    {
        Rule rule = this[old_priority];
        
        RemoveAt(old_priority);
        Insert(new_priority, rule);
    }
}
