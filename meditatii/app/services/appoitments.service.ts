import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { map } from "rxjs/operators";
import { Observable } from 'rxjs';
import { HttpClientModule, HttpClient, HttpHeaders } from '@angular/common/http';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
@Injectable()

export class AppoitmentsService
{
    constructor(private http: HttpClient )
    {
        console.log('MessagesService initialized...');
    }

    getAppoitments(page: number) {
        return this.http.get('/api/appoitments/getappoitments/' + page).pipe(map(
            (res:any) => res
        ))
    }
    
    getActiveAppoitments(page: number) {
        return this.http.get('/api/appoitments/getactiveappoitments/' + page).pipe(map(
            (res:any) => res
        ))
    }
    
    getOldAppoitments(page: number) {
        return this.http.get('/api/appoitments/getoldappoitments/' + page).pipe(map(
            (res:any) => res
        ))
	}
    
    saveAppoitment(teacherId:number, startDate:Date, endDate:Date)
    {
        return this.http.post('api/appoitments/SaveAppoitment', {TeacherId : teacherId.toString(), StartDate : startDate, EndDate: endDate}, httpOptions);
    }

    getChatForAppoitment(appoitmentid:number)
    {
        return this.http.get('/api/appoitments/getchatforappoitment/' + appoitmentid).pipe(map(
            (res:any) => res
        ))
    }

    deleteAppoitment(appoitmentid:number)
    {
        return this.http.get('api/appoitments/deleteappoitment/'+ appoitmentid).pipe(map(
            (res:any) => res
        ))
    }

    saveTeacherRating(teacherid:number, rate:number, ratingtext:string)
    {
        return this.http.post('api/teacherrating/SaveRatingForTeacher', {TeacherIdEncoded : teacherid.toString(), Rating: rate.toString(), RatingText: ratingtext}, httpOptions);
    }

    payment(listOfAppoitments:number[])
    {
        let body = JSON.stringify( listOfAppoitments );

        return this.http.post('api/appoitments/savepayments', body, httpOptions);
    }

    acceptByTeacher(appoitmentid:number)
    {
        return this.http.get('api/appoitments/acceptByTeacher/'+ appoitmentid).pipe(map(
            (res:any) => res
        ));
    }

    getCurrentPayment() {
        return this.http.get('/api/appoitments/getcurrentpayment/').pipe(map(
            (res:any) => res
        ))
    }

    getPaymentForUser() {
        return this.http.get('/api/appoitments/getpaymentforuser/').pipe(map(
            (res:any) => res
        ))
    }

    realpayment()
    {
        //construct the xml
        //go to the payment page
    }

}