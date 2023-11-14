import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { map } from "rxjs/operators";
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  const httpOptionsMulti = {
    headers: new HttpHeaders({ 'Content-Type': 'multipart/form-data' })
  };
@Injectable()
export class UsersService
{
    constructor(private http: HttpClient)
    {
        console.log('UserService initialized...');
    }

    getAds(categoryid: number, cycleid: number, cityid: number, order:number, page: number) {
        return this.http.get('/api/users/getusersdescription?categoryid=' + (categoryid == null ? '0' : categoryid) + '&cycleid=' + (cycleid == null ? '0' : cycleid) + '&cityid=' + (cityid == null ? '0' : cityid) + '&order=' + (order == null ? '0' : order)+ '&page=' + page).pipe(map(
            (res:any) => res
        ))
    }


    getUsers(categoryid: number, cycleid: number, cityid: number, order:number, page: number) {
        return this.http.get('/api/users/GetUsers?categoryid=' + (categoryid == null ? '0' : categoryid) + '&cycleid=' + (cycleid == null ? '0' : cycleid) + '&cityid=' + (cityid == null ? '0' : cityid) + '&order=' + (order == null ? '0' : order)+ '&page=' + page).pipe(map(
            (res:any) => res
        ))
    }

    getUser(userid: number) {
        return this.http.get('/api/users/GetUser?userid=' + (userid == null ? '0' : userid)).pipe(map(
            (res:any) => res
        ))
    }

    adView(adid:any)
    {
        return this.http.get('/api/users/AdView?adCode=' + (adid == null ? '0' : adid)).pipe(map(
            (res:any) => res
        ))
    }

    getNrOfViewsForAd(adid:any)
    {
        return this.http.get('/api/users/getNrOfViewsForAd?adCode=' + (adid == null ? '0' : adid)).pipe(map(
            (res:any) => res
        ))
    }

    getAdByCode(adid: any) {
        return this.http.get('/api/users/getadbycode?adid=' + (adid == null ? '0' : adid)).pipe(map(
            (res:any) => res
        ))
    }

    getUserByCode(userid: any) {
        return this.http.get('/api/users/getuserbycode?userid=' + (userid == null ? '0' : userid)).pipe(map(
            (res:any) => res
        ))
    }

    getUserPhoneNumber(userid: any) {
        return this.http.get('/api/users/getUserPhoneNumber?userid=' + (userid == null ? '0' : userid)).pipe(map(
            (res:any) => res
        ));
    }

    getUserAvaiabilityForDay(userid: any, day: any) {
        return this.http.get('/api/users/getavailabilityforday?userid=' + (userid == null ? '0' : userid + '&day=' + day)).pipe(map(
            (res:any) => res
        ))
    }

    getRatingsForTeacher(userid: any) {
        return this.http.get('/api/teacherrating/getallratingforteacher?teacherid=' + (userid == null ? '0' : userid)).pipe(map(
            (res:any) => res
        ))
    }

    getRecomandations(userid: any, categoryid: any, cityid: any) {
        return this.http.get('/api/users/GetSuggestedUsers?userid=' + userid + 'categoryid=' + categoryid  + '&cityid=' + cityid).pipe(map(
            (res:any) => res
        ))
    }

    saveAppoitment(teacherid:any, selecteddate:string, time: number)
    {
        return this.http.post('/api/users/saveappoitment?teacherId=' + (teacherid == null ? '0' : teacherid + '&selectedDate=' + selecteddate  + '&startTime=' + time),null);
    }

    getRequests(city:any, subject:any, page:number)
    {
        return this.http.get('/api/users/GetRequests?city=' + (city==null ? "" : city)  + '&subject=' + (subject == null ? "" : subject) + "&page=" + page).pipe(map(
            (res:any) => res
        ))
    }

    getRequest(id:any)
    {
        return this.http.get('/api/users/GetRequest?id=' + (id==null ? "" : id) ).pipe(map(
            (res:any) => res
        ))
    }

    saveRequest(newRequet:any) 
    {        
        //return this.http.post('api/users/SaveCurrentProfile', profile, httpOptions);
        return this.http.post('api/users/SaveNewRequest', newRequet, httpOptions);
    }

}