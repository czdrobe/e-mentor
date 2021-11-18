import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { map } from "rxjs/operators";
import { HttpClient, HttpHeaders } from '@angular/common/http';
const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
@Injectable()

export class MessagesService
{
    constructor(private http: HttpClient)
    {
        console.log('MessagesService initialized...');
    }

    getMessages(mentorId:number, page: number) {
        return this.http.get('/api/messages/getmessages/' + mentorId + '/' + page).pipe(map(
            (res:any) => res
        ))
    }
    
    getMessagesByMentorCode(mentorId:any, page: number) {
        return this.http.get('/api/messages/getmessagesbymentorcode/' + mentorId + '/' + page).pipe(map(
            (res:any) => res
        ))
	}

	getMentors()
	{
		return this.http.get('api/messages/listofmentors').pipe(map(
			(res:any) => res
		))
    }

    getUsers()
	{
		return this.http.get('api/messages/getlistofuserswithmessage').pipe(map(
			(res:any) => res
		))
    }
    
    saveMessage(toId:any, body:string)
    {
        var formData = new FormData();
        formData.append('ToUserId',toId.toString());
        formData.append('Body',body);
        return this.http.post('api/messages/SaveNewMessage', {ToUserId : toId.toString(), Body : body}, httpOptions);
    }

    saveMessageForRequest(requestId:string, message:string)
    {
        return this.http.post('api/messages/saveMessageForRequest', {requestId : requestId, message : message}, httpOptions);
    }
}