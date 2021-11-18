import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';
@Pipe({name: 'niceDateFormatPipe'})
export class NiceDateFormatPipe implements PipeTransform {

    transform(value: string) {
       
        var valueDate = new Date(value);
        var _value = valueDate.getTime();
        
        var dif = Math.floor( ( (Date.now() - _value) / 1000 ) / 86400 );
        
        if ( dif < 30 ){
             return convertToNiceDate(value);
        }else{
            var datePipe = new DatePipe("en-US");
            value = datePipe.transform(value, 'MMM-dd-yyyy');
            return value;
        }
     }
}

function convertToNiceDate(time: string) {
    var date = new Date(time),
        diff = (((new Date()).getTime() - date.getTime()) / 1000),
        daydiff = Math.floor(diff / 86400);

    if (isNaN(daydiff) || daydiff < 0 || daydiff >= 31)
        return '';

    return daydiff == 0 && (
        diff < 60 && "Chiar acum" ||
        diff < 120 && "1 minut in urmă" ||
        diff < 3600 && Math.floor(diff / 60) + " minute in urmă" ||
        diff < 7200 && "1 ora in urmă" ||
        diff < 86400 && Math.floor(diff / 3600) + " ore in urmă") ||
        daydiff == 1 && "Ieri" ||
        daydiff < 7 && daydiff + " zile in urmă" ||
        daydiff < 31 && Math.ceil(daydiff / 7) + " saptamani in urmă";
}