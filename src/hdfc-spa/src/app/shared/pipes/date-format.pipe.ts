import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({
  name: 'dateFormat',
})
export class DateFormatPipe implements PipeTransform {
  transform(value: string): any {
    const gmtDateTime = moment(value, 'YYYY-MM-DD');

    if (moment(gmtDateTime).format('hh:mm:ss a') === '12:00:00 am') {
      const local = gmtDateTime.local().format('ll');
      return local;
    } else {
      const local = gmtDateTime.local().format('ll h:mm A');
      return local;
    }

    // if (value != null && value !== undefined && value !== '') {
    //   return moment(gmtDateTime).format('ll h:mm A'); // --> localStorage.getItem('dateFormat')
    // }
  }
}
