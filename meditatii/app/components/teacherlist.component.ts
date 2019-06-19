import { Component } from '@angular/core';
import { UsersService } from '../services/users.service';
import { ProfileService } from '../services/profile.service';
import { CategoryService } from '../services/category.service';
import { CycleService } from '../services/cycle.service';
import { Router, ActivatedRoute } from '@angular/router';
import * as _ from 'underscore';
import { Cycle } from '../types/cycle.type';
import { Category } from '../types/categories.type';
import { MessagesService } from '../services/messages.service';
import {NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';


import { PagerService } from '../services/pager.service';

@Component({
    moduleId: module.id,
    selector: 'teacherlist',
    templateUrl: 'teacherlist.component.html',
    providers: [UsersService, CategoryService, CycleService, PagerService, MessagesService, ProfileService]
})
export class TeacherlistComponent {
    searchMaterii:string;
    searchCycle:string;
    teachers: Teacher[];
    categories: Category[];
    subCategories: Category[];
    cycles: Cycle[];
    selectedMainCategory: number;
    selectedCategory: number;
    selectedCategoryName: string;
    selectedCycle: number;
    selectedCycleName: string;
    // pager object
    pager: any = {};
    currentpage: number;
    selectedUserId:number;
    closeResult:string;
    newMessage: string;
    isCurrentUserIsLoggedIn: boolean;

    constructor(
        private userService: UsersService,
        private categoryService: CategoryService,
        private cycleService: CycleService,
        private router: Router,
        private activateRoute: ActivatedRoute,
        private pagerService: PagerService,
        private messagesService: MessagesService,
        private modalService: NgbModal,
        private profileService: ProfileService,
        //private ddService: NgbDropdown,
    )
    {
        
    }

    ngOnInit() {
        this.selectedCategoryName = "Alege o materie";
        this.selectedCycleName = "Alege ciclul scolar";
        
        // load main categories
        this.profileService.getCurrentProfile().subscribe((profile:any) => {
            this.isCurrentUserIsLoggedIn = profile != null;
            this.categoryService.getallwithsubcategories().subscribe((cats:any) => {
                console.log(cats);
                this.categories = cats;
                this.searchMaterii = ""; //in fact we reset the search

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
        });

        // load cycles
        this.cycleService.getCycles().subscribe((cycles:any) => {
            console.log(cycles);
            this.cycles = cycles;
        })
    }

    updateUrl()
    {
        this.router.navigate(['/teacher'], { queryParams: { maincategory: this.selectedMainCategory, category: this.selectedCategory, cycle: this.selectedCycle, page: this.currentpage } });
    }

    selectMainCategory(id: number)
    {
        console.log('main');
        this.selectedMainCategory = id;
        this.categoryService.getSubCategories(id).subscribe((cats:any) => {
            //console.log('subCategories:' + cats);
            this.subCategories = cats

            this.updateUrl();
        })
    }
    selectCategory(id: number, categorytName:string)
    {
        //console.log('categories');
        //this.ddService.
        this.selectedCategory = id;
        this.selectedCategoryName = categorytName;
        this.currentpage = 1;
        this.updateUrl();

        
    }

    selectCycle(id: number, cycleName: string)
    {
        this.selectedCycle = id;
        this.selectedCycleName = cycleName;
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
        this.userService.getUsers(this.selectedCategory, this.selectedCycle, page).subscribe((usersResult:any) => {
            console.log(usersResult);

            // get pager object from service
            this.pager = this.pagerService.getPager(usersResult.TotalRows, page);

            this.teachers = usersResult.Entities;
        });
    }

    selectUser(userId:number)
    {
        this.selectedUserId = userId;
    }

    open(content:any, options: any) {
		this.modalService.open(content, options).result.then((result) => {
		  this.closeResult = `Closed with: ${result}`;
		}, (reason) => {
		  this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
		});
	  }
	  
	  private getDismissReason(reason: any): string {
		if (reason === ModalDismissReasons.ESC) {
		  return 'by pressing ESC';
		} else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
		  return 'by clicking on a backdrop';
		} else {
		  return  `with: ${reason}`;
		}
      }
      
      sendMessage()
      {
          this.messagesService.saveMessage(this.selectedUserId, this.newMessage).subscribe(result => 
              {
                  this.newMessage = "";	
              });
          console.log('newmessage value:' + this.newMessage);
      }
}

interface Teacher {
    Id: number,
    firstName: string,
    lastName: string,
    email: string,
    description: string,
    Categories: Category[]
}