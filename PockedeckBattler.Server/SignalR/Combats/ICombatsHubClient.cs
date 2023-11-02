using PockedeckBattler.Server.Views;

namespace PockedeckBattler.Server.SignalR.Combats;

public interface ICombatsHubClient : IHubClient
{
    Task CombatInPreparationCreated(CombatInPreparationView view);
    Task CombatInPreparationChanged(CombatInPreparationView view);
    Task CombatInPreparationDeleted(CombatInPreparationView view);

    Task CombatCreated(PlayerCombatView view);
    Task CombatStarted(PlayerCombatView view);
    Task CombatUpdated(PlayerCombatView view);
    Task CombatOver(PlayerCombatView view);
}
