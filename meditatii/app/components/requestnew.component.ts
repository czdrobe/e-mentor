import { Component } from '@angular/core';
import { UsersService } from '../services/users.service';
import { Router, ActivatedRoute } from '@angular/router';
import { CategoryService } from '../services/category.service';
import * as _ from 'underscore';
import {NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { Category } from '../types/categories.type';
import { City } from '../types/city.type';
import { CycleService } from '../services/cycle.service';
import { ProfileService } from '../services/profile.service';


import { PagerService } from '../services/pager.service';
import { Cycle } from '../types/cycle.type';
import { Request, RequestMethod } from '@angular/http';
import { StringLiteralLike } from 'typescript';

@Component({
    moduleId: module.id,
    selector: 'requestnew',
    templateUrl: 'html/requestnew.component.html',
    providers: [UsersService, PagerService, CycleService, CategoryService, ProfileService]
})

export class RequestnewComponent {
    closeResult:string;    
    categories: Category[];
    cycles: Cycle[];
    cities: City[];
    selectedCycle: number;
    selectedCity: number;
    selectedCycleName: string;
    selectedCityName: string;
    selectedCategoryName: string;
    selectedCategory: number;

    
    newMessage:string;
    isOnline: boolean;
    duration:number;

    
    errorMessage: string;
    currentUser: any;
  
    constructor(
        private categoryService: CategoryService,
        private userService: UsersService,
        private modalService: NgbModal,
        private activateRoute: ActivatedRoute,
        private pagerService: PagerService,
        private router: Router,
        private profileService: ProfileService,
        private cycleService: CycleService,
        
    )
    {

    }

    ngOnInit() {
      
      this.isOnline = false; 

      this.selectedCategoryName = "Alege o materie";
      this.selectedCycleName = "Alege ciclul scolar";
      this.selectedCityName = "Alege o localitate";

      this.profileService.getCurrentProfile().subscribe((results:any) => {
        console.log(results);
        this.currentUser = results;

        if (this.currentUser == null)
        {
          this.router.navigate(['/Account/Login'], { queryParams : {}}).then(() => {
            window.location.reload();
          });
        }
        else
        {
          this.categoryService.getallwithsubcategories().subscribe((cats:any) => {
            this.categories = cats;
            });
    
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
      });

     
    }

    selectCategory(id: number, categorytName:string)
    {
        this.selectedCategory = id;
        this.selectedCategoryName = categorytName;
    }
    selectCity(id: number, cityName: string)
    {
        this.selectedCity = id;
        this.selectedCityName = cityName;
    }

    saveNewRequest()
    {

      var isvalid = true;
      this.errorMessage = "";

      if (this.selectedCategory===undefined || this.selectedCategory==null || this.selectedCategory <= 0)
      {
        this.errorMessage = "Alege o materie";
        isvalid = false;
      }
      else if (!this.isOnline && (this.selectedCity ===undefined || this.selectedCity==null || this.selectedCity <= 0) )
      {
        this.errorMessage = "Alege un oras";
        isvalid = false;
      }
      else if (this.newMessage ===undefined || this.newMessage == "")
      {
        this.errorMessage = "Te rog introdu ce anume cauți ?";
        isvalid = false;
      }
      else if (this.duration ===undefined || this.duration < 0)
      {
        this.errorMessage = "Alege durata solicitarii";
        isvalid = false;
      }
      else if (this.selectedCycle ===undefined || this.selectedCycle==null || this.selectedCycle <= 0)
      {
        this.errorMessage = "Alege categoria de varsta";
        isvalid = false;
      }

      if (isvalid)
      {
        var newRequest =
        {
          CategoryId: this.selectedCategory,
          IsOnline: this.isOnline,
          CityId: this.selectedCity,
          Description: this.newMessage,
          CycleId: this.selectedCycle,
          Duration: this.duration
          //duration ?
        }

        this.userService.saveRequest(newRequest).subscribe(result => 
          {
            console.log("Saved");
            window.location.href = "/solicitare-de-meditatii/" + result;
          });  
      }
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
}

interface RequestModel {
    Id: number,
    IsOnline:boolean,
    Description: string,
    LearnerId:number,
    StartDate:Date,
    EndDate:Date,
    Active:Boolean,
    Category: Category,
    CategoryId:number,
    CityId:number,
    City:City,
    RequestCode:String
}