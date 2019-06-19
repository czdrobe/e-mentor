import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { map } from "rxjs/operators";
import { HttpClient } from '@angular/common/http';

@Injectable()
export class UsersService
{
    constructor(private http: HttpClient)
    {
        console.log('UserService initialized...');
    }

    getUsers(categoryid: number, cycleid: number, page: number) {
        return this.http.get('/api/users/GetUsers?categoryid=' + (categoryid == null ? '0' : categoryid) + '&cycleid=' + (cycleid == null ? '0' : cycleid) + '&page=' + page).pipe(map(
            (res:any) => res
        ))
    }

    getUser(userid: number) {
        return this.http.get('/api/users/GetUser?userid=' + (userid == null ? '0' : userid)).pipe(map(
            (res:any) => res
        ))
    }

    getUserAvaiabilityForDay(userid: number, day: any) {
        return this.http.get('/api/users/getavailabilityforday?userid=' + (userid == null ? '0' : userid + '&day=' + day)).pipe(map(
            (res:any) => res
        ))
    }

    saveAppoitment(teacherid:number, selecteddate:string, time: number)
    {
        return this.http.post('/api/users/saveappoitment?teacherId=' + (teacherid == null ? '0' : teacherid + '&selectedDate=' + selecteddate  + '&startTime=' + time),null);
    }
}