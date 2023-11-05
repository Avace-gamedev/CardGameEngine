export const assetIcons = [
  'screen-impact',
  'random-dice',
  'three-leaves',
  'fire',
  'water-drop',
  'sandstorm',
  'd10',
  'd6',
  'dice-1-6',
  'dice-2-6',
  'dice-3-6',
  'dice-4-6',
  'dice-5-6',
  'dice-6-6',
  'galaxy',
  'shield',
  'heal',
  'heart-shield',
  'healing-shield',
  'health-increase',
  'heart-plus',
  'heart-minus',
  'armor-upgrade',
  'armor-downgrade',
  'dice-increase',
  'dice-decrease',
  'biceps',
  'broken-axe',
  'cracked-shield',
  'shield-reflect',
  'fangs',
  'stopwatch',
  'duration',
  'vibrating-shield',
  'yin-yang',
] as const;
export type AssetIcon = (typeof assetIcons)[number];
