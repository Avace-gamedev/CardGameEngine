import { AssetIcon } from '../asset-icon/asset-icons';
import { Element } from '../../../api/pockedeck-battler-api-client';

export class ElementIconsUtils {
  static getIcon(element: Element): AssetIcon | undefined {
    switch (element) {
      case Element.Neutral:
        return 'yin-yang';
      case Element.Fire:
        return 'three-leaves';
      case Element.Earth:
        return 'fire';
      case Element.Water:
        return 'water-drop';
      case Element.Wind:
        return 'sandstorm';
    }
  }
}
