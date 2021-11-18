import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { map } from "rxjs/operators";
import { Observable } from 'rxjs';
import { HttpClientModule, HttpClient, HttpHeaders } from '@angular/common/http';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
@Injectable()

export class ReportService
{
    constructor(private http: HttpClient )
    {
        console.log('ReportService initialized...');
    }

    getAppoitmentReport(page: number) {
        return this.http.get('/api/report/getteacherappoitmentreport/' + page).pipe(map(
            (res:any) => res
        ))
    }

    getBalance()
    {
        return this.http.get('/api/report/getbalance/').pipe(map(
            (res:any) => res
        ))
    }
    
}