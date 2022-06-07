namespace PGChatBot;

public record LinkData(string Link, params string[] Modifiers)
{
    public string RandomModifier()
    {
        return Modifiers.Length > 0 ? Modifiers[Random.Shared.Next(0, Modifiers.Length)] : "";
    }
}