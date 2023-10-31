using CardGame.Engine.Characters;

namespace PockedeckBattler.Server.Content.Characters.Attributes;

/// <summary>
///     Mark a <see cref="Character" /> as starter.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class StarterAttribute : Attribute
{
}
