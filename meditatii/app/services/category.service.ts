import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class CategoryService {
    constructor(private http: Http) {
        console.log('CategoryService initialized...');
    }

    getCategories() {
        return this.http.get('/api/categories/getall').map(
            res => res.json()
        )
    }

    getMainCategories() {
        return this.http.get('/api/categories/getmains').map(
            res => res.json()
        )
    }

    getSubCategories(id: number) {
        return this.http.get('/api/categories/getsubcategories?id=' + id).map(
            res => res.json()
        )
    }

}