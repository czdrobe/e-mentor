import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class UsersService
{
    constructor(private http: Http)
    {
        console.log('UserService initialized...');
    }

    getUsers(categoryid: number, cycleid: number, page: number) {
        return this.http.get('/api/users/GetUsers?categoryid=' + (categoryid == null ? '0' : categoryid) + '&cycleid=' + (cycleid == null ? '0' : cycleid) + '&page=' + page).map(
            res => res.json()
        )
    }
}