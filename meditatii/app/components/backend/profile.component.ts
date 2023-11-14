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

@Component({
	moduleId: module.id,
	templateUrl: 'html/profile.component.html',
	providers: [CycleService, CategoryService, ProfileService,ImageCropperComponent, {provide: NgbDateAdapter, useClass: NgbDateNativeAdapter}],
	selector: 'ngbd-datepicker-popup'
})
export class ProfileComponent {
	data: any;
	cropperSettings: CropperSettings;
	cropper:         ImageCropperComponent;
	closeResult:     string;
	editmodeprofile: boolean;
	editmodecity: boolean;
	editmodeaddress: boolean;
	editmodedescription: boolean;
	editmodeprice: boolean;
	editmodeStudii: boolean;
	editmodeavaiability: boolean;
	model:           any;
	copymodel:       any;
	availability:    any[];
	mytime: any;
	ismeridian: boolean;
	categories: Category[];
	cycles: Cycle[];
	selectedCityName1: string;
	selectedCity1: number;

	selectedExperienceName: string;
	selectedExperience: number;

	selectedOccupationName: string;
	selectedOccupation: number;

	bIsOnline: boolean;
	bIsOnStudent:boolean;
	bIsOnTeacher:boolean;

	lstExperience:Experience[];
	lstOccupation:Occupation[];

	lstCity:City[];
	lstFilteredCity:City[];

	searchCity: string;

	lstCounty:string[];
	selectedCountyName:string;
	
	errorCity: string;
	errorStudies:string;
	errorExperience: string;
	errorOccupation: string;
    	
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
		this.cropperSettings = new CropperSettings();
        this.cropperSettings.width = 160;
        this.cropperSettings.height = 160;
        this.cropperSettings.croppedWidth = 160;
        this.cropperSettings.croppedHeight = 160;
        this.cropperSettings.canvasWidth = 400;
		this.cropperSettings.canvasHeight = 300;
		this.cropperSettings.rounded = true;
    
		this.data = {};
		this.editmodeprofile = false;
		this.editmodecity = false;
		this.editmodeStudii = false;
	}
	
	changed()
	{

	}

	ngOnInit() {
		this.mytime =  new Date();
		this.ismeridian = true;

		this.selectedCityName1 = "Alege o localitate";

		this.selectedExperienceName = "Alege experienta";
		this.selectedOccupationName = "Alege ocupatia";

		this.activateRoute.queryParams.subscribe(params => {
			if (params.hasOwnProperty("message"))
			{

			}
		});

		this.profileService.getCurrentProfile().subscribe((results:any) => {
			this.model = results;
			this.searchCity = "";

			this.bIsOnline = this.model.AlsoOnline;
			this.bIsOnTeacher = this.model.AtTeacher;
			this.bIsOnStudent = this.model.AtStudent;

			this.resetEditCity();
			this.updateFromModel();

			if (this.model.Dob != null)
			{
				var dob = new Date(this.model.Dob);
				this.model.Dob =  dob;
			}
			console.log(this.model);

			this.categoryService.getCategories().subscribe((cats:any) => {
				console.log(cats);
				cats.forEach((element:any) => {
					var foundElements = this.model.Categories.filter((x:Category) => x.Id == element.Id);
					element.selected =  foundElements.length > 0;
				});
				this.categories = cats;
			})
	
			// load cycles
			this.cycleService.getCycles().subscribe((cycles:any) => {
				console.log(cycles);
				cycles.forEach((element:any) => {
					var foundElements = this.model.Cycles.filter((x:Cycle) => x.Id == element.Id);
					element.selected =  foundElements.length > 0;
				});
				this.cycles = cycles;
			})
			// load cities
			this.profileService.getCities().subscribe((results:any) => {
				console.log(results);
				this.lstCity = results;
				var temp: string[] = [];
				this.lstCity.forEach(function(item){
					if (!temp.some(x => x == item.County))
					{
						temp.push(item.County);
					}
				});
				this.lstCounty = temp;
			})

			// load experience
			this.profileService.getExperience().subscribe((results:any) => {
				console.log(results);
				this.lstExperience = results;
			})
			
			// load occupation
			this.profileService.getOccupation().subscribe((results:any) => {
				console.log(results);
				this.lstOccupation = results;
			})			
		});

		
	}

	selectExperience(id: number, cityName: string)
    {
        this.selectedExperience = id;
        this.selectedExperienceName = cityName;
		
		if (this.model.Experience == null)
		{
			this.model.Experience = new Object();
		}
		this.model.Experience.Id = id;
    }
	
	selectOccupation(id: number, cityName: string)
    {
        this.selectedOccupation = id;
        this.selectedOccupationName = cityName;
		if (this.model.Occupation == null)
		{
			this.model.Occupation = new Object();
		}
		this.model.Occupation.Id = id;
    }

	selectCity1(id: number, cityName: string)
    {
        this.selectedCity1 = id;
        this.selectedCityName1 = cityName;
    }
	selectCounty(county: string)
    {
        this.selectedCountyName = county;

		this.lstFilteredCity = this.lstCity.filter( (item) => 
		{
			return item.County  == county;
		})

    }	
	
	resetCity1()
	{
        this.selectedCity1 = 0;
        this.selectedCityName1 = "Alege o localitate";
	}

	saveCity()
	{
		if ( (this.selectedCity1 === undefined || this.selectedCity1 <= 0) && !this.bIsOnline)
		{
			this.errorCity = "Te rog alege un oras sau meditatii online";
		}
		else
		{
			this.errorCity = "";
			this.profileService.saveCityForCurrentProfie(this.selectedCity1, this.bIsOnTeacher, this.bIsOnStudent, this.bIsOnline).subscribe(result => 
				{
					console.log("Saved");
					
					window.location.href = window.location.href + "?refreshuser=1";
				});	
		}

	}

	saveProfile()
	{
		this.saveProfileToDB(true);
		//this.editProfile();
		//reload page to refresh user in session
		//this.router.navigate(['/u/profile'], { queryParams: { refreshuser: 1 } });
	}

	saveAddress()
	{
		this.saveProfileToDB(false);
		this.editAddress();
	}

	saveDescription()
	{
		this.saveProfileToDB(false);
		this.editDescription();
	}

	savePrice()
	{
		this.saveProfileToDB(false);
		this.editPrice();
	}
	saveStudii()
	{
		this.errorStudies = "";

		if (this.model.Studies == null || this.model.Studies == "")
		{
			this.errorStudies = "Acest camp este obligatoriu!";
		}
		else
		{
			this.saveProfileToDB(false);
			this.editStudii();
		}
	}
	saveAva()
	{
		this.saveAvailabilityToDB();
		this.editAva();
	}

	saveAvailabilityToDB()
	{
		var tempAvailability: any[] = [];
		this.availability.forEach((element:any) => {
			if (element.startTime > 0 && element.endTime > 0 && element.endTime > element.startTime)
			{
				for (var i = element.startTime; i< element.endTime; i++ )
				{
					tempAvailability.push({
						Day:element.day,
						Time: i
					});
				}
			}
		});
		this.profileService.saveAvaiability(tempAvailability).subscribe(result => 
			{
				console.log("Saved");
			});
	}
	saveProfileToDB(reload:boolean)
	{
		
		this.errorExperience = "";
		this.errorOccupation = "";
		if (this.model.Dob != null)
		{
			if (typeof this.model.Dob == "string")
			{
				var dobFromString = new Date(this.model.Dob);
				this.model.Dob =  dobFromString;
			}
			var dob = new Date(Date.UTC(this.model.Dob.getFullYear(), this.model.Dob.getMonth(), this.model.Dob.getDate()));
			this.model.Dob =  dob;
		}

		if (this.selectedExperience <= 0)
		{
			this.errorExperience = "Alegeti o experienta";
		}
		else if (this.selectedOccupation <= 0)
		{
			this.errorOccupation = "Alegeti o ocpatie";
		}
		else
		{
			this.profileService.saveCurrentProfie(this.model).subscribe(result => 
				{
					console.log("Saved");
					if (reload)
					{
						window.location.href = window.location.href + "?refreshuser=1";
					}
				});
		}
	}

	editCity()
	{
		this.editmodecity = !this.editmodecity;
		if (this.editmodecity)
		{
			this.bIsOnline = this.model.AlsoOnline;
			this.copymodel = JSON.parse(JSON.stringify(this.model));
			
		}
	}

	editProfile()
	{
		this.editmodeprofile = !this.editmodeprofile;
		if (this.editmodeprofile)
		{
			this.copymodel = JSON.parse(JSON.stringify(this.model));
		}
	}
	
	editAddress()
	{
		this.editmodeaddress = !this.editmodeaddress;
		if (this.editmodeaddress)
		{
			this.copymodel = JSON.parse(JSON.stringify(this.model));
		}
	}

	editDescription()
	{
		this.editmodedescription = !this.editmodedescription;
		if (this.editmodedescription)
		{
			this.copymodel = JSON.parse(JSON.stringify(this.model));
		}
	}

	editStudii()
	{
		this.editmodeStudii = !this.editmodeStudii;
		if (this.editmodeStudii)
		{
			this.copymodel = JSON.parse(JSON.stringify(this.model));
		}
	}

	editPrice()
	{
		this.editmodeprice = !this.editmodeprice;
		if (this.editmodeprice)
		{
			this.copymodel = JSON.parse(JSON.stringify(this.model));
		}
	}

	editAva()
	{
		this.editmodeavaiability = !this.editmodeavaiability;
		if (this.editmodeavaiability)
		{
			this.copymodel = JSON.parse(JSON.stringify(this.model));
		}
	}

	updateFromModel()
	{
		if (this.model.Experience != null)
		{
			this.selectedExperience = this.model.Experience.Id;
			this.selectedExperienceName = this.model.Experience.Name;	
		}

		if (this.model.Occupation != null)
		{
			this.selectedOccupation = this.model.Occupation.Id;
			this.selectedOccupationName = this.model.Occupation.Name;	
		}
	}

	cancelEditProfile()
	{
		this.model = JSON.parse(JSON.stringify(this.copymodel));
		this.updateFromModel();
		this.editProfile();	
	}
	
	cancelEditCity()
	{
		this.errorCity = "";
		this.model = JSON.parse(JSON.stringify(this.copymodel));
		this.bIsOnline = this.model.AlsoOnline;
		this.resetEditCity();
		this.editCity();
	}

	resetEditCity()
	{
		if (this.model.Cities.length > 0)
		{
			this.selectedCountyName = this.model.Cities[0].County;
			this.selectedCity1 = this.model.Cities[0].Id;
			this.selectedCityName1 = this.model.Cities[0].Name;
		}

	}

	cancelEditAddress()
	{
		this.model = JSON.parse(JSON.stringify(this.copymodel));
		this.editAddress();	
	}

	cancelEditDescription()
	{
		this.model = JSON.parse(JSON.stringify(this.copymodel));
		this.editDescription();
	}

	cancelEditStudii()
	{
		this.model = JSON.parse(JSON.stringify(this.copymodel));
		this.editStudii();
	}

	cancelEditPrice()
	{
		this.model = JSON.parse(JSON.stringify(this.copymodel));
		this.editPrice();
	}
	
	cancelAva()
	{
		this.model = JSON.parse(JSON.stringify(this.copymodel));
		this.editAva();
	}

	saveCategories()
	{
		this.profileService.saveCategories(this.categories.filter( (element:Category) => element.selected) ).subscribe(result => 
			{
				console.log("Saved");
			});
		console.log(this.categories);
	}

	saveCycles()
	{
		this.profileService.saveCycles(this.cycles.filter( (element:Cycle) => element.selected) ).subscribe(result => 
			{
				console.log("Saved"); 
			});
		console.log(this.cycles);
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
	
	fileChangeListener(event:any) {
		var image:any = new Image();
		var file:File = event.target.files[0];
		var myReader:FileReader = new FileReader();
		var that = this;
		myReader.onloadend = function (loadEvent:any) {
			image.src = loadEvent.target.result;
			that.cropper.setImage(image);
	
		};
	
		myReader.readAsDataURL(file);
	}

	saveImage()
	{	  
		this.profileService.saveProfileImage(this.data).subscribe(result => 
			{
				console.log("Saved image"); 
				this.profileService.getCurrentProfile().subscribe((results:any) => {
					this.model = results;
					this.model.ProfileImageUrl += '?random+\=' + Math.random();//force reload
					console.log("Profile reloaded"); 
				});
			});
	}

}