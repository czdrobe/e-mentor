import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class CycleService {
    constructor(private http: Http) {
        console.log('CycleService initialized...');
    }

    getCycles() {
        return this.http.get('/api/cycle/getall').map(
            res => res.json()
        )
    }
}