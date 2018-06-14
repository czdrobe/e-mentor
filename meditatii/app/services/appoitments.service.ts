import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { map } from "rxjs/operators";
import { Observable } from 'rxjs';

const httpOptions = {
    headers: new Headers({ 'Content-Type': 'application/json' })
  };
@Injectable()

export class AppoitmentsService
{
    constructor(private http: Http)
    {
        console.log('MessagesService initialized...');
    }

    getAppoitments(page: number) {
        return this.http.get('/api/appoitments/getappoitments/' + page).pipe(map(
            (res:any) => res.json()
        ))
	}
    
    saveAppoitment(teacherId:number, startDate:Date, endDate:Date)
    {
        return this.http.post('api/appoitments/SaveNewMessage', {TeacherId : teacherId.toString(), StartDate : startDate, EndDate: endDate}, httpOptions);
    }
}