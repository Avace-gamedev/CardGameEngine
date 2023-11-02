using PockedeckBattler.Server.Views;

namespace PockedeckBattler.Server.SignalR.Combats;

public interface ICombatsHubClient : IHubClient
{
    Task CombatInPreparationChange(CombatInPreparationView view);
}
