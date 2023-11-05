using CardGame.Engine.Effects.Enchantments.State;
using CardGame.Engine.Effects.Enchantments.Triggered;

namespace CardGame.Engine.Effects.Enchantments;

public class Enchantment
{
    public Enchantment(string name, params PassiveEffect[] passive) : this(name, passive, null) { }
    public Enchantment(string name, params TriggeredEffect[] triggered) : this(name, null, triggered) { }

    public Enchantment(string name, IReadOnlyList<PassiveEffect>? passive, IReadOnlyList<TriggeredEffect>? triggered)
    {
        Name = name;
        Passive = passive ?? Array.Empty<PassiveEffect>();
        Triggered = triggered ?? Array.Empty<TriggeredEffect>();
    }

    public string Name { get; }
    public IReadOnlyList<PassiveEffect> Passive { get; }
    public IReadOnlyList<TriggeredEffect> Triggered { get; }
}
