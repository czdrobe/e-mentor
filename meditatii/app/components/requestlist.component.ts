import { Component } from '@angular/core';
import { UsersService } from '../services/users.service';
import { Router, ActivatedRoute } from '@angular/router';
import * as _ from 'underscore';
import {NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { Category } from '../types/categories.type';
import { City } from '../types/city.type';


import { PagerService } from '../services/pager.service';

@Component({
    moduleId: module.id,
    selector: 'requestlist',
    templateUrl: 'html/requestlist.component.html',
    providers: [UsersService, PagerService]
})

export class RequestlistComponent {
    closeResult:string;
    selectedCity:string;
    selectedSubject:string;
    currentpage: number;
    pager: any = {};
    requests: Request[];
    nrOfRequests:number;
    constructor(
        private userService: UsersService,
        private modalService: NgbModal,
        private activateRoute: ActivatedRoute,
        private pagerService: PagerService,
        private router: Router,
    )
    {

    }

    ngOnInit() {

        this.activateRoute.queryParams.subscribe(params => {

            if (params.hasOwnProperty("city")) {
                this.selectedCity = params.city;
            }
            if (params.hasOwnProperty("subject")) {
                this.selectedSubject = params.subject;
            }

            this.currentpage = (params.hasOwnProperty("page") ? parseInt(params.page) : 1);

            this.setPage(this.currentpage);
        });

    }

    updateUrl()
    {
        this.router.navigate(['/solicitare-de-meditatii'], { queryParams: { subject: this.selectedSubject, city: this.selectedCity, page: this.currentpage } });
    }

    new()
    {
      this.router.navigate(['/adauga-solicitare-noua'], { queryParams: {  } })
    }


    setPage(page: number) {
        if (page < 1) {
            return;
        }
        this.currentpage = page;
        this.userService.getRequests(this.selectedCity, this.selectedSubject, page).subscribe((urequestsResult:any) => {
            console.log(urequestsResult);

            // get pager object from service
            this.pager = this.pagerService.getPager(urequestsResult.TotalRows, page);

            this.requests = urequestsResult.Entities;

            this.nrOfRequests = urequestsResult.TotalRows;
        });
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

interface Request {
    Id: number,
    IsOnline:boolean,
    Description: string,
    LearnerId:number,
    StartDate:Date,
    EndDate:Date,
    Active:Boolean,
    Category: Category,
    City:City,
    RequestCode:String
}