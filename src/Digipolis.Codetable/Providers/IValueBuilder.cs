namespace Digipolis.Codetable
{
    public interface IValueBuilder
    {
        string GetValueOrDefault(string value, string defaultValue);
    }
}
