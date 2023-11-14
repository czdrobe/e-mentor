import { Component, ElementRef } from '@angular/core';
import { UsersService } from '../services/users.service';
import { ProfileService } from '../services/profile.service';
import { CategoryService } from '../services/category.service';
import { CycleService } from '../services/cycle.service';
import { Router, ActivatedRoute } from '@angular/router';
import * as _ from 'underscore';
import { Cycle } from '../types/cycle.type';
import { City } from '../types/city.type';
import { Category } from '../types/categories.type';
import { Ad } from '../types/ad.type';
import { MessagesService } from '../services/messages.service';
import {NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';


import { PagerService } from '../services/pager.service';

@Component({
    moduleId: module.id,
    selector: 'teacherlist',
    templateUrl: 'html/teacherlist.component.html',
    providers: [UsersService, CategoryService, CycleService, PagerService, MessagesService, ProfileService]
})
export class TeacherlistComponent {
    searchMaterii:string;
    searchCity: string;
    searchCycle:string;
    ads: Ad[];
    collapse:boolean[];
    nrOfTeachers: number;
    categories: Category[];
    subCategories: Category[];
    lstCategoryGroupped: Category[];
    cycles: Cycle[];
    cities:City[];
    selectedMainCategory: number;
    selectedCategory: number;
    selectedCategoryName: string;
    selectedCategoryNameWithoutMain: string;
    selectedCycle: number;
    selectedCity: number;
    selectedCycleName: string;
    selectedCityName: string;
    // pager object
    pager: any = {};
    currentpage: number;
    selectedUserId:number;
    closeResult:string;
    newMessage: string;
    isCurrentUserIsLoggedIn: boolean;
    selectedOrderName:string;
    selectedOrder:number;
    orders:Order[];
    selectedUrlCategory:string;
    selectedUrlCategoryName:string;

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
        rootElement: ElementRef
    )
    {
        this.selectedUrlCategory ="";
        this.selectedUrlCategoryName ="";
        var domElementCat = document.getElementById("catId");
        if (domElementCat != null)
        {
            this.selectedUrlCategory = domElementCat.getAttribute("value");
            this.selectedCategory = Number(this.selectedUrlCategory);
        }
        var domElementCatName = document.getElementById("catName");
        if (domElementCatName != null)
        {
            this.selectedUrlCategoryName = domElementCatName.getAttribute("value");
        }
    
        this.orders = [ {Id:1, Name:'Cele mai populare'}, {Id:2, Name:'Review-uri'}, {Id:3, Name:'Cele mai noi'}];
    }

    ngOnInit() {
        console.log(this.selectedUrlCategory);
        console.log(this.selectedUrlCategoryName);
        this.selectedCategoryName = "Alege o materie";
        this.selectedCycleName = "Alege ciclul scolar";
        this.selectedOrderName = "Cele mai populare";
        this.selectedCityName = "Alege o localitate";

        this.selectedOrder = 1;
        
        // load main categories
        this.profileService.getCurrentProfile().subscribe((profile:any) => {
            this.isCurrentUserIsLoggedIn = profile != null;
            this.categoryService.getallwithsubcategories().subscribe((cats:any) => {
                console.log(cats);
                this.categories = cats;
                this.searchMaterii = ""; //in fact we reset the search
                this.searchCity = "";
                this.activateRoute.params.subscribe(params => {
                    if (params.hasOwnProperty("maincategory"))
                    {

                    }
                    
                });

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
                    if (params.hasOwnProperty("city")) {
                        this.selectedCity = params.city;
                    }
                    if (params.hasOwnProperty("order")) {
                        this.selectedOrder = params.order;
                    }

                    this.currentpage = (params.hasOwnProperty("page") ? parseInt(params.page) : 1);

                    this.setPage(this.currentpage);
                });
            })
        });

        this.categoryService.getCategoriesGroupped().subscribe((cats:any) => {
			console.log(cats);
			this.lstCategoryGroupped = cats;
		})

        // load cycles
        this.cycleService.getCycles().subscribe((cycles:any) => {
            console.log(cycles);
            this.cycles = cycles;
        })

        // load cities
        this.profileService.getCities().subscribe((cities:any) => {
            console.log(cities);
            this.cities = cities;
        })        
    }
    unSelectCity()
    {
        this.selectedCity = null;
        this.selectedCityName = null;
        
        this.cities.forEach(function (element:City) {
            //console.log(element);
            element.selected = false;
        });

        this.updateUrl();
    }

    unSelectCycle()
    {
        this.selectedCycle = null;
        this.selectedCycleName = null;

        this.cycles.forEach(function (element) {
            element.selected = false;
        });

        this.updateUrl();
    }
    
    resetFilters()
    {
        this.selectedOrderName = "Cele mai populare";
        this.selectedCategoryName = "Alege o materie";
        this.selectedCycleName = "Alege ciclul scolar";
        this.selectedCityName = "Alege o localitate";
        this.selectedMainCategory = null;
        this.selectedCategory = null;
        this.selectedCycle = null;
        this.selectedCity = null;
        this.currentpage = 1;
        this.updateUrl();
    }

    updateUrl()
    {
        this.router.navigate([], { queryParams: { cycle: this.selectedCycle, city: this.selectedCity, order: this.selectedOrder, page: this.currentpage } });
    }

    selectMainCategory(id: number)
    {
        console.log('main');
        this.selectedMainCategory = id;
        this.categoryService.getSubCategories(id).subscribe((cats:any) => {
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
        /*if (categorytName.indexOf(' - ')>-1)
        {
            this.selectedCategoryNameWithoutMain = categorytName.substring(categorytName.indexOf(' - ') +3, categorytName.length);
        }
        else
        {
            this.selectedCategoryNameWithoutMain = categorytName;
        }
        console.log(categorytName);
        console.log(this.selectedCategoryNameWithoutMain);
        */
        this.currentpage = 1;
        this.updateUrl();        
    }

    selectOrder(id: number, orderName:string)
    {
        this.selectedOrder = id;
        this.selectedOrderName = orderName;
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

    selectCity(city: City)
    {
        this.selectedCity = city.Id;
        this.selectedCityName = city.Name;
        city.selected = !city.selected;
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

        
        this.userService.getAds(this.selectedCategory, this.selectedCycle, this.selectedCity, this.selectedOrder, page).subscribe((usersResult:any) => {
            console.log(usersResult);

            // get pager object from service
            this.pager = this.pagerService.getPager(usersResult.TotalRows, page);

            this.ads = usersResult.Entities;

            this.nrOfTeachers = usersResult.TotalRows;
        });

        /*
        this.userService.getUsers(this.selectedCategory, this.selectedCycle, this.selectedCity, this.selectedOrder, page).subscribe((usersResult:any) => {
            console.log(usersResult);

            // get pager object from service
            this.pager = this.pagerService.getPager(usersResult.TotalRows, page);

            this.teachers = usersResult.Entities;

            this.nrOfTeachers = usersResult.TotalRows;
        });*/
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

interface Order {
    Id:number,
    Name:string
}