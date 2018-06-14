import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { map } from "rxjs/operators";

@Injectable()
export class CategoryService {
    constructor(private http: Http) {
        console.log('CategoryService initialized...');
    }

    getCategories() {
        return this.http.get('/api/categories/getall').pipe(map(
            (res:any) => res.json()
        ))
    }

    getMainCategories() {
        return this.http.get('/api/categories/getmains').pipe(map(
            (res:any) => res.json()
        ))
    }

    getSubCategories(id: number) {
        return this.http.get('/api/categories/getsubcategories?id=' + id).pipe(map(
            (res:any) => res.json()
        ))
    }

}