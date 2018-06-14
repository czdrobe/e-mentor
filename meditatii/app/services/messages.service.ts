import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { map } from "rxjs/operators";
const httpOptions = {
    headers: new Headers({ 'Content-Type': 'application/json' })
  };
@Injectable()

export class MessagesService
{
    constructor(private http: Http)
    {
        console.log('MessagesService initialized...');
    }

    getMessages(mentorId:number, page: number) {
        return this.http.get('/api/messages/getmessages/' + mentorId + '/' + page).pipe(map(
            (res:any) => res.json()
        ))
	}

	getMentors()
	{
		return this.http.get('api/messages/listofmentors').pipe(map(
			(res:any) => res.json()
		))
    }
    
    saveMessage(toId:number, body:string)
    {
        var formData = new FormData();
        formData.append('ToUserId',toId.toString());
        formData.append('Body',body);
        return this.http.post('api/messages/SaveNewMessage', {ToId : toId.toString(), Body : body}, httpOptions);
    }
}