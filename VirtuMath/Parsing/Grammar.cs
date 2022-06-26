namespace VirtuMath.Parsing;

public class Grammar : List<Rule>
{
    public Grammar() : base() { }

    public Grammar(IEnumerable<Rule> rules) : base(rules) { }

    public void ChangePriority(int old_priority, int new_priority)
    {
        Rule rule = this[old_priority];
        
        RemoveAt(old_priority);
        Insert(new_priority, rule);
    }
}
