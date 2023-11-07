using CardGame.Engine.Effects.Enchantments.Passive;
using CardGame.Engine.Effects.Enchantments.Triggered;

namespace CardGame.Engine.Effects.Enchantments;

public class Enchantment
{
    public Enchantment(string name, IReadOnlyList<PassiveEffect>? passive, IReadOnlyList<TriggeredEffect>? triggered)
    {
        Name = name;
        Passive = passive ?? Array.Empty<PassiveEffect>();
        Triggered = triggered ?? Array.Empty<TriggeredEffect>();
    }

    public string Name { get; }
    public IReadOnlyList<PassiveEffect> Passive { get; }
    public IReadOnlyList<TriggeredEffect> Triggered { get; }

    public static Enchantment CreateInstance(string name, params PassiveEffect[] passive)
    {
        return new Enchantment(name, passive, null);
    }

    public static Enchantment CreateInstance(string name, params TriggeredEffect[] triggered)
    {
        return new Enchantment(name, null, triggered);
    }
}
