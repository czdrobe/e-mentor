import { Component } from '@angular/core';
import { UsersService } from '../services/users.service';
import { CategoryService } from '../services/category.service';
import { CycleService } from '../services/cycle.service';
import { Router, ActivatedRoute } from '@angular/router';
import * as _ from 'underscore';

import { PagerService } from '../services/pager.service';

@Component({
    moduleId: module.id,
    selector: 'teacherlist',
    templateUrl: 'teacherlist.component.html',
    providers: [UsersService, CategoryService, CycleService, PagerService]
})
export class TeacherlistComponent {
    teachers: Teacher[];
    categories: Category[];
    subCategories: Category[];
    cycles: Cycle[];
    selectedMainCategory: number;
    selectedCategory: number;
    selectedCycle: number;
    // pager object
    pager: any = {};
    currentpage: number;

    constructor(
        private userService: UsersService,
        private categoryService: CategoryService,
        private cycleService: CycleService,
        private router: Router,
        private activateRoute: ActivatedRoute,
        private pagerService: PagerService
    )
    {
        
    }

    ngOnInit() {
        // load main categories
        this.categoryService.getMainCategories().subscribe(cats => {
            console.log(cats);
            this.categories = cats

            this.activateRoute.queryParams.subscribe(params => {
                if (params.hasOwnProperty("maincategory"))
                {
                    this.selectedMainCategory = params.maincategory;
                    this.selectMainCategory(this.selectedMainCategory)
                }
                if (params.hasOwnProperty("category")) {
                    this.selectedCategory = params.category;
                }
                if (params.hasOwnProperty("cycle")) {
                    this.selectedCycle = params.cycle;
                }

                this.currentpage = (params.hasOwnProperty("page") ? parseInt(params.page) : 1);

                this.setPage(this.currentpage);
            });
        })

        // load cycles
        this.cycleService.getCycles().subscribe(cycles => {
            console.log(cycles);
            this.cycles = cycles;
        })
    }

    updateUrl()
    {
        this.router.navigate(['/Teacher'], { queryParams: { maincategory: this.selectedMainCategory, category: this.selectedCategory, cycle: this.selectedCycle, page: this.currentpage } });
    }

    selectMainCategory(id: number)
    {
        console.log('main');
        this.selectedMainCategory = id;
        this.categoryService.getSubCategories(id).subscribe(cats => {
            //console.log('subCategories:' + cats);
            this.subCategories = cats

            this.updateUrl();
        })
    }
    selectCategory(id: number)
    {
        //console.log('categories');
        this.selectedCategory = id;
        this.currentpage = 1;
        this.updateUrl();
    }

    selectCycle(id: number)
    {
        this.selectedCycle = id;
        this.currentpage = 1;
        this.updateUrl();
    }

    setCurrentPage(page: number)
    {
        this.currentpage = page;
        this.updateUrl();
    }

    setPage(page: number) {
        if (page < 1) {
            return;
        }
        this.currentpage = page;
        this.userService.getUsers(this.selectedCategory, this.selectedCycle, page).subscribe(usersResult => {
            console.log(usersResult);

            // get pager object from service
            this.pager = this.pagerService.getPager(usersResult.TotalRows, page);

            this.teachers = usersResult.Entities;
        });
    }
}

interface Teacher {
    firstName: string,
    lastName: string,
    email: string,
    description: string,
    Categories: Category[]
}

interface Category {
    Name:string
}

interface Cycle {
    Id:number,
    name: string
}