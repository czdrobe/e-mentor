import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class MessagesService
{
    constructor(private http: Http)
    {
        console.log('MessagesService initialized...');
    }

    getMessages(page: number) {
        return this.http.get('/api/messages/getmessages?page=' + page).map(
            res => res.json()
        )
    }
}