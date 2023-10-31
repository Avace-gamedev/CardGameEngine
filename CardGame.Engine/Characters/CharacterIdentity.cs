namespace CardGame.Engine.Characters;

public class CharacterIdentity
{
    public CharacterIdentity(string name, string displayName, string? description = null)
    {
        Name = name;
        DisplayName = displayName;
        Description = description;
    }

    public string Name { get; }
    public string DisplayName { get; }
    public string? Description { get; }
}
