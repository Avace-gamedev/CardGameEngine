interface String {
  trimEndChars: (...chars: string[]) => string;
}

String.prototype.trimEndChars = function (...chars: string[]) {
  let i = this.length - 1;
  while (i >= 0 && chars.includes(this[i])) {
    i++;
  }

  return this.substring(i);
};
