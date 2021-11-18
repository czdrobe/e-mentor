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

	selectedCityName2: string;
	selectedCity2: number;

	selectedCityName3: string;
	selectedCity3: number;

	bIsOnline: boolean;

	lstCity:City[];
	lstCity2:City[];
	lstCity3:City[];
	searchCity: string;
	searchCity2: string;
	searchCity3: string;

	errorCity: string;
    
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
	}
	
	changed()
	{

	}

	ngOnInit() {
		this.mytime =  new Date();
		this.ismeridian = true;

		this.selectedCityName1 = "Alege o localitate";
		this.selectedCityName2 = "Alege o localitate";
		this.selectedCityName3 = "Alege o localitate";

		this.profileService.getCurrentProfile().subscribe((results:any) => {
			this.model = results;
			this.searchCity = "";

			this.bIsOnline = this.model.AlsoOnline;
			this.resetEditCity();

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
				this.lstCity2 = results;
				this.lstCity3 = results;

			})
		});

		this.profileService.getAvailability().subscribe( (results:any) => {
			this.availability =[];
			var tempAvailability: any[] = [];
			var startTime: number = 0;
			var endTime: number = 0;
			var day:number = 0;
			var totalElements: number = results.Entities.length;
			var i:number = 0;
			results.Entities.forEach((element:any) => {
				i++;
				if (day == 0)
				{
					day = element.Day;
					startTime = element.Time;
				}
				if (day != element.Day || i == totalElements) //create also for last element
				{
					if (i == totalElements)
					{
						endTime = element.Time;
					}

					//we have one day starttime and endtime so create the object
					tempAvailability.push({
						day: day,
						startTime: startTime,
						endTime: endTime + 1
					}); 

					day = element.Day;
					startTime = element.Time;
				}
				endTime = element.Time;
			});

			for (var j:number=1;j<=7; j++)
			{
				var availability = tempAvailability.filter(i=>i.day == j)[0];
				var dayName = "Luni";
				switch(j)
				{
					case 1:
						dayName ="Luni";break;
					case 2:
						dayName = "Marti";break;
						case 3:
						dayName = "Miercuri";break;
						case 4:
						dayName = "Joi";break;
						case 5:
						dayName = "Vineri";break;
						case 6:
						dayName = "Sambata";break;
						case 7:
						dayName = "Duminica";break;
				}
				this.availability.push({
					day:j,
					dayName: dayName,
					startTime:(availability!=null ? availability.startTime : 0),
					endTime:(availability!=null ? availability.endTime : 0),
					available: (availability!=null && availability.startTime > 0 && availability.endTime) ? 1 : 0
				})
			}
		});


	}

	selectCity1(id: number, cityName: string)
    {
        this.selectedCity1 = id;
        this.selectedCityName1 = cityName;
    }

	selectCity2(id: number, cityName: string)
    {
        this.selectedCity2 = id;
        this.selectedCityName2 = cityName;
    }

	selectCity3(id: number, cityName: string)
    {
        this.selectedCity3 = id;
        this.selectedCityName3 = cityName;
    }
	
	resetCity1()
	{
        this.selectedCity1 = 0;
        this.selectedCityName1 = "Alege o localitate";
	}

	resetCity2()
	{
        this.selectedCity2 = 0;
        this.selectedCityName2 = "Alege o localitate";
	}

	resetCity3()
	{
        this.selectedCity3 = 0;
        this.selectedCityName3 = "Alege o localitate";
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
			this.profileService.saveCityForCurrentProfie(this.selectedCity1, this.selectedCity2, this.selectedCity3, this.bIsOnline).subscribe(result => 
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
		this.profileService.saveCurrentProfie(this.model).subscribe(result => 
			{
				console.log("Saved");
				if (reload)
				{
					window.location.href = window.location.href + "?refreshuser=1";
				}
			});
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

	cancelEditProfile()
	{
		this.model = JSON.parse(JSON.stringify(this.copymodel));
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
			this.selectedCity1 = this.model.Cities[0].Id;
			this.selectedCityName1 = this.model.Cities[0].Name;
		}

		if (this.model.Cities.length > 1)
		{
			this.selectedCity2 = this.model.Cities[1].Id;
			this.selectedCityName2 = this.model.Cities[1].Name;
		}

		if (this.model.Cities.length > 2)
		{
			this.selectedCity3 = this.model.Cities[2].Id;
			this.selectedCityName3 = this.model.Cities[2].Name;
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