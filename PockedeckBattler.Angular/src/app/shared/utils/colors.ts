type KnownColor = 'white' | 'black';
type CssVarColor = `--${string}`;
type HexColor = `#${string}`;
type RgbColor = `rgb(${number}, ${number}, ${number})`;
type RgbaColor = `rgba(${number}, ${number}, ${number}, ${number})`;
type ColorMixColor = `color-mix(${string})`;

export type Color = KnownColor | CssVarColor | HexColor | RgbColor | RgbaColor | ColorMixColor;

const isCssVarColor = (color: Color): color is CssVarColor => color.startsWith('--');

export const getCssColor = (color: Color): string => {
  if (isCssVarColor(color)) {
    return `var(${color[1]})`;
  } else {
    return color;
  }
};
