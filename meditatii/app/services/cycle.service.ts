import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { map } from "rxjs/operators";

@Injectable()
export class CycleService {
    constructor(private http: Http) {
        console.log('CycleService initialized...');
    }

    getCycles() {
        return this.http.get('/api/cycle/getall').pipe(map(
            (res:any) => res.json()
        ))
    }
}