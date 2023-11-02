namespace PockedeckBattler.Server.Stores.CombatsInPreparation;

public class CombatInPreparation
{
    public CombatInPreparation(Guid id, string leftPlayerName)
    {
        Id = id;
        LeftPlayerName = leftPlayerName;
    }

    public Guid Id { get; }
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
