export const assetIcons: readonly string[] = [
  'screen-impact',
  'random-dice',
] as const;
export type AssetIcon = (typeof assetIcons)[number];
