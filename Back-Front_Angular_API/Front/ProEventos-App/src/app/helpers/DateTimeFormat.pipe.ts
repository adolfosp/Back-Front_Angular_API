import { Constants } from './../util/constants';
import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'DateTimeFormat'
  // esse name não pode ter o mesmo valor que o nome da classe
})
export class DateTimeFormatPipe extends DatePipe implements PipeTransform {
//sobrescreve pipe DATEPIPE
  transform(value: any, args?: any): any {
    return super.transform(value, Constants.DATE_TIME_FORMAT);
  }

}
