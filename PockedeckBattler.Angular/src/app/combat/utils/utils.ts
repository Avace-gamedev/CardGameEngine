import { ActionCardTarget } from '../../api/pockedeck-battler-api-client';

export class ActionCardTargetUtils {
  static getAllyTargets(
    target: ActionCardTarget,
    sourcePosition: 'front' | 'back',
  ): 'none' | 'front' | 'back' | 'both' {
    switch (target) {
      case ActionCardTarget.Self:
        return sourcePosition;
      case ActionCardTarget.OtherAlly:
        return sourcePosition === 'front' ? 'back' : 'front';
      case ActionCardTarget.FrontAlly:
        return 'front';
      case ActionCardTarget.BackAlly:
        return 'back';
      case ActionCardTarget.AllAllies:
        return 'both';
      case ActionCardTarget.None:
      case ActionCardTarget.FrontOpponent:
      case ActionCardTarget.BackOpponent:
      case ActionCardTarget.AllOpponents:
        return 'none';
    }
  }
  static getEnemyTargets(
    target: ActionCardTarget,
  ): 'none' | 'front' | 'back' | 'both' {
    switch (target) {
      case ActionCardTarget.FrontOpponent:
        return 'front';
      case ActionCardTarget.BackOpponent:
        return 'back';
      case ActionCardTarget.AllOpponents:
        return 'both';
      case ActionCardTarget.None:
      case ActionCardTarget.Self:
      case ActionCardTarget.OtherAlly:
      case ActionCardTarget.FrontAlly:
      case ActionCardTarget.BackAlly:
      case ActionCardTarget.AllAllies:
        return 'none';
    }
  }
}
