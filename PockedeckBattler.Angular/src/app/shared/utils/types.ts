export enum CardType {
  None = 'none',
  Damage = 'damage',
  Heal = 'heal',
  Shield = 'shield',
  Enchantment = 'enchantment',
}

export enum ActiveEffectType {
  None = 'none',
  Damage = 'damage',
  Heal = 'heal',
  Shield = 'shield',
  Random = 'random',
  AddEnchantment = 'add-passive',
}

export enum PassiveEffectType {
  None = 'non',
  CharacterStats = 'character-stats',
  CardStats = 'card-stats',
}
