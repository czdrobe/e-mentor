import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import 'rxjs/add/operator/map';
const httpOptions = {
    headers: new Headers({ 'Content-Type': 'application/json' })
  };
@Injectable()

export class ProfileService
{
    constructor(private http: Http)
    {
        console.log('MessagesService initialized...');
    }

}