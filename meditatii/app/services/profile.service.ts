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

    saveProfileImage(image:any)
    {
        var formData = new FormData();
        formData.append('profileimage', image.image);
        return this.http.post('/api/users/saveprofileimage', formData);
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