namespace CardGame.Engine.Cards;

public abstract class Card
{
    public Card(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; }
    public string Description { get; }
}
