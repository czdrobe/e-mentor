import { Component } from '@angular/core';
import { ProfileService } from '../../services/profile.service';
import { CategoryService } from '../../services/category.service';
import { CycleService } from '../../services/cycle.service';
import { Router, ActivatedRoute } from '@angular/router';
import * as _ from 'underscore';
import {MessageModel} from '../../models/messageModel';
import {ImageCropperComponent,CropperSettings} from 'ng2-img-cropper';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import {NgbDateStruct, NgbCalendar, NgbDateAdapter, NgbDateNativeAdapter} from '@ng-bootstrap/ng-bootstrap';
import { Cycle } from '../../types/cycle.type';
import { Category } from '../../types/categories.type';
import { elementAt } from 'rxjs/operators';
import { City } from '../../types/city.type';
import { Experience } from '../../types/experience.type';
import { Occupation } from '../../types/occupation.type';
import { Ad } from '../../types/ad.type';

@Component({
	moduleId: module.id,
	templateUrl: 'html/home.component.html',
	providers: [CycleService, CategoryService, ProfileService,ImageCropperComponent, {provide: NgbDateAdapter, useClass: NgbDateNativeAdapter}],
	selector: 'ngbd-datepicker-popup'
})
export class UserHomeComponent {
	lstAds:Ad[];
	selectedCategoryName:string;
	selectedDurationName:string;
	lstDuration:Duration[];

	selectedCategoryId:number;
	selectedDurationId:number;

	closeResult:string;
	lstCategory: Category[];

	selected:string;

	newMessage:string;

	price:number;
	added: any;

	cycles: Cycle[];

	editedAdId:string;
	modalRef:any;
	error:string;

	constructor(
		private calendar: NgbCalendar,
		private profileService: ProfileService,
    	private router: Router,
		private activateRoute: ActivatedRoute,
		private modalService: NgbModal,
		private categoryService: CategoryService,
        private cycleService: CycleService,
    	)
    {		
		this.lstDuration = [
			{id:60, name:'o ora'},
			{id:90, name:'o ora si 30 de minute'},
			{id:120, name:'doua ore'},
			{id:150, name:'doua ore si 30 de minute'},
			{id:180, name:'trei ore'},
		];
	}
	
	changed()
	{

	}

	ngOnInit() {

		this.profileService.getAdsForCurrentUser().subscribe((results:any) => {
			this.lstAds = results;
		});

		this.categoryService.getCategoriesGroupped().subscribe((cats:any) => {
			console.log(cats);
			this.lstCategory = cats;
		})

		// load cycles
		this.cycleService.getCycles().subscribe((cycles:any) => {
			console.log(cycles);
			this.cycles = cycles;
		})
	}
    open(content:any, options: any) {
		this.modalRef = this.modalService.open(content);
		this.modalRef.result.then((result:any) => {
		  this.closeResult = `Closed with: ${result}`;
		}, (reasonnew:any) => {
			this.closeResult = `Dismissed ${this.getDismissReason(reasonnew)}`;
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

	newAd()
	{
		this.error ="";
		this.editedAdId = "";
		this.selectedCategoryId = 0;
		this.selectedCategoryName = "";
		this.price = 0;
		
		this.newMessage = "";
		this.selectedDurationId = 60;
		this.selectedDurationName = "";
		this.cycles.forEach((element:any) => {
			element.selected =  false;
		});

	}

	deleteAd(code:string)
	{
		if(confirm("Esti sigur ca vrei sa stergi acest anunt ?")) 
		{
			var newRequest =
			{
				Code:code,
			}

			this.profileService.deleteAd(newRequest).subscribe(result => 
			{
				console.log("Deleted");
				this.profileService.getAdsForCurrentUser().subscribe((results:any) => {
					this.lstAds = results;
				});
			});
		  }
	}

	editAd(code:string)
	{
		this.error ="";

		this.editedAdId = code;

		var ads = this.lstAds.filter(function(el) {
			return el.Code === code;
		});

		var ad = ads[0];

		this.selectedCategoryId = ad.Category.Id;
		this.selectedCategoryName = ad.Category.Name;

		var durations = this.lstDuration.filter(function(el) {
			return el.id === ad.Duration;
		});
		var duration = durations[0];

		this.price = ad.Price;
		this.added = ad.Added;
		this.newMessage = ad.Description;
		this.selectedDurationId = duration.id;
		this.selectedDurationName = duration.name;

		//cycle
		this.cycles.forEach((element:any) => {
					var foundElements = ad.Cycles.filter((x:Cycle) => x.Id == element.Id);
					element.selected =  foundElements.length > 0;
				});
	}

	saveAd()
	{
		console.log("save Ad");

		var adCycles:Cycle[] = [];

		this.cycles.forEach((cycle:Cycle) => {
			if (cycle.selected)
			{
				adCycles.push(cycle);
			}
		})

		this.error = "";
		//validate		
		if (this.selectedCategoryId <= 0)
		{
			this.error = "Selecteaza o materie!";
		}
		else if (this.newMessage == "")
		{
			this.error = "Adauga o descriere!";
		}
		else if (adCycles.length <=0 )
		{
			this.error = "Selecteaza cel putin 1 nivel!"
		}

		if (this.error == "")
		{
			var newRequest =
			{
				Code:this.editedAdId,
				Category: {Id: this.selectedCategoryId},
				Duration: this.selectedDurationId,
				Price: this.price,
				Added:this.added,
				Description: this.newMessage,
				Cycles:adCycles
			}
			let localModalRef = this.modalRef;
			this.profileService.saveAd(newRequest).subscribe(result => 
				{
					
					console.log("Saved");
					this.profileService.getAdsForCurrentUser().subscribe((results:any) => {
						this.lstAds = results;
					});
	
					localModalRef.close();
				});
		}

	}

}

interface Duration{
	id:number,
	name:string
}