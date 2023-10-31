using System.Reflection;
using CardGame.Engine.Characters;
using PockedeckBattler.Server.Content.Characters.Attributes;

namespace PockedeckBattler.Server.Content.Characters;

public static partial class Characters
{
    static readonly Dictionary<string, Character> AllCharacters;
    static readonly Dictionary<string, Character> StarterCharacters;

    static Characters()
    {
        // Dict construction must happen in the constructor to ensure all the other static fields have been initialized

        StarterCharacters = GetCharacterProperties()
            .Where(p => p.GetCustomAttributes(typeof(StarterAttribute), false).Any())
            .Select(p => p.GetValue(null))
            .OfType<Character>()
            .ToDictionary(c => c.Identity.Name, c => c);
        AllCharacters = GetCharacterProperties().Select(p => p.GetValue(null)).OfType<Character>().ToDictionary(c => c.Identity.Name, c => c);
    }

    public static IEnumerable<Character> All => AllCharacters.Values;
    public static IEnumerable<Character> Starters => StarterCharacters.Values;

    public static Character? GetByName(string name)
    {
        return AllCharacters.GetValueOrDefault(name);
    }

    public static Character RequireByName(string name)
    {
        return GetByName(name) ?? throw new Exception($"Character {name} not found");
    }

    public static Character? GetStarterByName(string name)
    {
        return StarterCharacters.GetValueOrDefault(name);
    }

    static IEnumerable<PropertyInfo> GetCharacterProperties()
    {
        return typeof(Characters).GetProperties().Where(p => typeof(Character).IsAssignableFrom(p.PropertyType));
    }
}
