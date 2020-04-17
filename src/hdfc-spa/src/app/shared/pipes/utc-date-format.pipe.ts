import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({
  name: 'utcDateFormat',
})
export class UtcDateFormatPipe implements PipeTransform {
  transform(value: any, args?: any): any {
    const gmtDateTime = moment.utc(value, 'YYYY-MM-DD  HH:mm:ss');
    if (moment(gmtDateTime).format('hh:mm:ss a') === '12:00:00 am') {
      const local = gmtDateTime.local().format('ll');
      return local;
    } else {
      const local = gmtDateTime.local().format('ll h:mm A');
      return local;
    }
  }
}
