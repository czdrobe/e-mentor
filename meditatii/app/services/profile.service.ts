import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { map, catchError } from "rxjs/operators";
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError, fromEventPattern } from 'rxjs';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  const httpOptionsMulti = {
    headers: new HttpHeaders({ 'Content-Type': 'multipart/form-data' })
  };
@Injectable()

export class ProfileService
{
    constructor(private http: HttpClient)
    {
        console.log('ProfileService initialized...');
    }

    getCurrentProfile() {
        return this.http.get('/api/users/GetCurrentProfile').pipe(map(
            (res:any) => res
        ))
    }

    getAvailability() {
        return this.http.get('/api/users/getavailability').pipe(map(
            (res:any) => res
        ))
    }

    saveCurrentProfie(profile:any) 
    {        
        //return this.http.post('api/users/SaveCurrentProfile', profile, httpOptions);
        return this.http.post('api/users/SaveCurrentProfile', profile, httpOptions);
    }

    saveAvaiability(lstAvaibility:any)
    {
        return this.http.post('/api/users/saveavailability', lstAvaibility, httpOptions);
    }

    saveCategories(lstCategories:any)
    {
        return this.http.post('/api/users/savecategories', lstCategories, httpOptions);
    }
    
    saveCycles(lstCycles:any)
    {
        return this.http.post('/api/users/savecycles', lstCycles, httpOptions);
    }

    saveCityForCurrentProfie(city1:number, atTeacher:boolean, atStudent:boolean, isOnline:boolean)
    {
        return this.http.post('/api/users/savecityforcurrentprofie', {cityid1: city1, atTeacher: atTeacher, atStudent: atStudent, isOnline: isOnline}, httpOptions);
    }

    saveProfileImage(image:any)
    {
        var formData = new FormData();
        formData.append('profileimage', image.image);
        return this.http.post('/api/users/saveprofileimage', formData);
    }

    getCities()
    {
        return this.http.get('/api/users/GetCities').pipe(map(
            (res:any) => res
        ))
    }

    getExperience()
    {
        console.log('start inExperience');
        return this.http.get('/api/users/GetExperience').pipe(map(
            (res:any) => res
        ));
    }
    
    getOccupation()
    {
        console.log('start inOccupation');
        return this.http.get('/api/users/GetOccupation').pipe(map(
            (res:any) => res
        ));
    }

    getAdsForCurrentUser()
    {
        return this.http.get('/api/users/getadsforcurrentuser').pipe(map(
            (res:any) => res
        ))
    }

    saveAd(adrequest:any)
    {
        return this.http.post('/api/users/savead', adrequest, httpOptions);
    }

    deleteAd(adrequest:any)
    {
        return this.http.post('/api/users/deletead', adrequest, httpOptions);
    }
    
    private handleError(error: HttpErrorResponse) {
        if (error.error instanceof ErrorEvent) {
          // A client-side or network error occurred. Handle it accordingly.
          console.error('An error occurred:', error.error.message);
        } else {
          // The backend returned an unsuccessful response code.
          // The response body may contain clues as to what went wrong,
          console.error(
            `Backend returned code ${error.status}, ` +
            `body was: ${error.error}`);
        }
        // return an observable with a user-facing error message
        return throwError(
          'Something bad happened; please try again later.');
      };

}