namespace PockedeckBattler.Server.Stores.CombatsInPreparation;

public class CombatInPreparation
{
    public CombatInPreparation(Guid id, string leftPlayerName, string? randomSeed = null)
    {
        Id = id;
        LeftPlayerName = leftPlayerName;
        RandomSeed = randomSeed;
    }

    public Guid Id { get; }
    public string? RandomSeed { get; set; }

    public string LeftPlayerName { get; }
    public string? LeftFrontCharacter { get; set; }
    public string? LeftBackCharacter { get; set; }
    public bool LeftReady { get; set; }

    public bool RightPlayerIsAi { get; set; }
    public string? RightPlayerName { get; set; }
    public string? RightFrontCharacter { get; set; }
    public string? RightBackCharacter { get; set; }
    public bool RightReady { get; set; }
}
