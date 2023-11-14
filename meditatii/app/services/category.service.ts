import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { map } from "rxjs/operators";
import { HttpClientModule, HttpClient } from '@angular/common/http';

@Injectable()
export class CategoryService {
    constructor(private http: HttpClient ) {
        console.log('CategoryService initialized...');
    }

    getCategories() {
        return this.http.get('/api/categories/getall').pipe(map(
            (res:any) => res
        ))
    }

    getCategoriesGroupped() { 
        return this.http.get('/api/categories/getallgroupped').pipe(map(
            (res:any) => res
        ))
    }

    getMainCategories() {
        return this.http.get('/api/categories/getmains').pipe(map(
            (res:any) => res
        ))
    }

    getallwithsubcategories() {
        return this.http.get('/api/categories/getallwithsubcategories').pipe(map(
            (res:any) => res
        ))
    }

    getSubCategories(id: number) {
        return this.http.get('/api/categories/getsubcategories?id=' + id).pipe(map(
            (res:any) => res
        ))
    }

}