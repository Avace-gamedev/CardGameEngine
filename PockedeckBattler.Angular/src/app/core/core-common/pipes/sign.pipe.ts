import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'sign',
})
export class SignPipe implements PipeTransform {
  transform(value: number): string {
    const sign: string = value >= 0 ? '+' : '';
    return `${sign}${value}`;
  }
}
