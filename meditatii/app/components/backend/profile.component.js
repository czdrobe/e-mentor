"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var profile_service_1 = require("../../services/profile.service");
var category_service_1 = require("../../services/category.service");
var cycle_service_1 = require("../../services/cycle.service");
var router_1 = require("@angular/router");
var ng2_img_cropper_1 = require("ng2-img-cropper");
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var ng_bootstrap_2 = require("@ng-bootstrap/ng-bootstrap");
var ProfileComponent = /** @class */ (function () {
    function ProfileComponent(calendar, profileService, router, activateRoute, modalService, categoryService, cycleService) {
        this.calendar = calendar;
        this.profileService = profileService;
        this.router = router;
        this.activateRoute = activateRoute;
        this.modalService = modalService;
        this.categoryService = categoryService;
        this.cycleService = cycleService;
        this.cropperSettings = new ng2_img_cropper_1.CropperSettings();
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
    ProfileComponent.prototype.changed = function () {
    };
    ProfileComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.mytime = new Date();
        this.ismeridian = true;
        this.selectedCityName1 = "Alege o localitate";
        this.selectedExperienceName = "Alege experienta";
        this.selectedOccupationName = "Alege ocupatia";
        this.activateRoute.queryParams.subscribe(function (params) {
            if (params.hasOwnProperty("message")) {
            }
        });
        this.profileService.getCurrentProfile().subscribe(function (results) {
            _this.model = results;
            _this.searchCity = "";
            _this.bIsOnline = _this.model.AlsoOnline;
            _this.bIsOnTeacher = _this.model.AtTeacher;
            _this.bIsOnStudent = _this.model.AtStudent;
            _this.resetEditCity();
            _this.updateFromModel();
            if (_this.model.Dob != null) {
                var dob = new Date(_this.model.Dob);
                _this.model.Dob = dob;
            }
            console.log(_this.model);
            _this.categoryService.getCategories().subscribe(function (cats) {
                console.log(cats);
                cats.forEach(function (element) {
                    var foundElements = _this.model.Categories.filter(function (x) { return x.Id == element.Id; });
                    element.selected = foundElements.length > 0;
                });
                _this.categories = cats;
            });
            // load cycles
            _this.cycleService.getCycles().subscribe(function (cycles) {
                console.log(cycles);
                cycles.forEach(function (element) {
                    var foundElements = _this.model.Cycles.filter(function (x) { return x.Id == element.Id; });
                    element.selected = foundElements.length > 0;
                });
                _this.cycles = cycles;
            });
            // load cities
            _this.profileService.getCities().subscribe(function (results) {
                console.log(results);
                _this.lstCity = results;
                var temp = [];
                _this.lstCity.forEach(function (item) {
                    if (!temp.some(function (x) { return x == item.County; })) {
                        temp.push(item.County);
                    }
                });
                _this.lstCounty = temp;
            });
            // load experience
            _this.profileService.getExperience().subscribe(function (results) {
                console.log(results);
                _this.lstExperience = results;
            });
            // load occupation
            _this.profileService.getOccupation().subscribe(function (results) {
                console.log(results);
                _this.lstOccupation = results;
            });
        });
    };
    ProfileComponent.prototype.selectExperience = function (id, cityName) {
        this.selectedExperience = id;
        this.selectedExperienceName = cityName;
        if (this.model.Experience == null) {
            this.model.Experience = new Object();
        }
        this.model.Experience.Id = id;
    };
    ProfileComponent.prototype.selectOccupation = function (id, cityName) {
        this.selectedOccupation = id;
        this.selectedOccupationName = cityName;
        if (this.model.Occupation == null) {
            this.model.Occupation = new Object();
        }
        this.model.Occupation.Id = id;
    };
    ProfileComponent.prototype.selectCity1 = function (id, cityName) {
        this.selectedCity1 = id;
        this.selectedCityName1 = cityName;
    };
    ProfileComponent.prototype.selectCounty = function (county) {
        this.selectedCountyName = county;
        this.lstFilteredCity = this.lstCity.filter(function (item) {
            return item.County == county;
        });
    };
    ProfileComponent.prototype.resetCity1 = function () {
        this.selectedCity1 = 0;
        this.selectedCityName1 = "Alege o localitate";
    };
    ProfileComponent.prototype.saveCity = function () {
        if ((this.selectedCity1 === undefined || this.selectedCity1 <= 0) && !this.bIsOnline) {
            this.errorCity = "Te rog alege un oras sau meditatii online";
        }
        else {
            this.errorCity = "";
            this.profileService.saveCityForCurrentProfie(this.selectedCity1, this.bIsOnTeacher, this.bIsOnStudent, this.bIsOnline).subscribe(function (result) {
                console.log("Saved");
                window.location.href = window.location.href + "?refreshuser=1";
            });
        }
    };
    ProfileComponent.prototype.saveProfile = function () {
        this.saveProfileToDB(true);
        //this.editProfile();
        //reload page to refresh user in session
        //this.router.navigate(['/u/profile'], { queryParams: { refreshuser: 1 } });
    };
    ProfileComponent.prototype.saveAddress = function () {
        this.saveProfileToDB(false);
        this.editAddress();
    };
    ProfileComponent.prototype.saveDescription = function () {
        this.saveProfileToDB(false);
        this.editDescription();
    };
    ProfileComponent.prototype.savePrice = function () {
        this.saveProfileToDB(false);
        this.editPrice();
    };
    ProfileComponent.prototype.saveStudii = function () {
        this.errorStudies = "";
        if (this.model.Studies == null || this.model.Studies == "") {
            this.errorStudies = "Acest camp este obligatoriu!";
        }
        else {
            this.saveProfileToDB(false);
            this.editStudii();
        }
    };
    ProfileComponent.prototype.saveAva = function () {
        this.saveAvailabilityToDB();
        this.editAva();
    };
    ProfileComponent.prototype.saveAvailabilityToDB = function () {
        var tempAvailability = [];
        this.availability.forEach(function (element) {
            if (element.startTime > 0 && element.endTime > 0 && element.endTime > element.startTime) {
                for (var i = element.startTime; i < element.endTime; i++) {
                    tempAvailability.push({
                        Day: element.day,
                        Time: i
                    });
                }
            }
        });
        this.profileService.saveAvaiability(tempAvailability).subscribe(function (result) {
            console.log("Saved");
        });
    };
    ProfileComponent.prototype.saveProfileToDB = function (reload) {
        this.errorExperience = "";
        this.errorOccupation = "";
        if (this.model.Dob != null) {
            if (typeof this.model.Dob == "string") {
                var dobFromString = new Date(this.model.Dob);
                this.model.Dob = dobFromString;
            }
            var dob = new Date(Date.UTC(this.model.Dob.getFullYear(), this.model.Dob.getMonth(), this.model.Dob.getDate()));
            this.model.Dob = dob;
        }
        if (this.selectedExperience <= 0) {
            this.errorExperience = "Alegeti o experienta";
        }
        else if (this.selectedOccupation <= 0) {
            this.errorOccupation = "Alegeti o ocpatie";
        }
        else {
            this.profileService.saveCurrentProfie(this.model).subscribe(function (result) {
                console.log("Saved");
                if (reload) {
                    window.location.href = window.location.href + "?refreshuser=1";
                }
            });
        }
    };
    ProfileComponent.prototype.editCity = function () {
        this.editmodecity = !this.editmodecity;
        if (this.editmodecity) {
            this.bIsOnline = this.model.AlsoOnline;
            this.copymodel = JSON.parse(JSON.stringify(this.model));
        }
    };
    ProfileComponent.prototype.editProfile = function () {
        this.editmodeprofile = !this.editmodeprofile;
        if (this.editmodeprofile) {
            this.copymodel = JSON.parse(JSON.stringify(this.model));
        }
    };
    ProfileComponent.prototype.editAddress = function () {
        this.editmodeaddress = !this.editmodeaddress;
        if (this.editmodeaddress) {
            this.copymodel = JSON.parse(JSON.stringify(this.model));
        }
    };
    ProfileComponent.prototype.editDescription = function () {
        this.editmodedescription = !this.editmodedescription;
        if (this.editmodedescription) {
            this.copymodel = JSON.parse(JSON.stringify(this.model));
        }
    };
    ProfileComponent.prototype.editStudii = function () {
        this.editmodeStudii = !this.editmodeStudii;
        if (this.editmodeStudii) {
            this.copymodel = JSON.parse(JSON.stringify(this.model));
        }
    };
    ProfileComponent.prototype.editPrice = function () {
        this.editmodeprice = !this.editmodeprice;
        if (this.editmodeprice) {
            this.copymodel = JSON.parse(JSON.stringify(this.model));
        }
    };
    ProfileComponent.prototype.editAva = function () {
        this.editmodeavaiability = !this.editmodeavaiability;
        if (this.editmodeavaiability) {
            this.copymodel = JSON.parse(JSON.stringify(this.model));
        }
    };
    ProfileComponent.prototype.updateFromModel = function () {
        if (this.model.Experience != null) {
            this.selectedExperience = this.model.Experience.Id;
            this.selectedExperienceName = this.model.Experience.Name;
        }
        if (this.model.Occupation != null) {
            this.selectedOccupation = this.model.Occupation.Id;
            this.selectedOccupationName = this.model.Occupation.Name;
        }
    };
    ProfileComponent.prototype.cancelEditProfile = function () {
        this.model = JSON.parse(JSON.stringify(this.copymodel));
        this.updateFromModel();
        this.editProfile();
    };
    ProfileComponent.prototype.cancelEditCity = function () {
        this.errorCity = "";
        this.model = JSON.parse(JSON.stringify(this.copymodel));
        this.bIsOnline = this.model.AlsoOnline;
        this.resetEditCity();
        this.editCity();
    };
    ProfileComponent.prototype.resetEditCity = function () {
        if (this.model.Cities.length > 0) {
            this.selectedCountyName = this.model.Cities[0].County;
            this.selectedCity1 = this.model.Cities[0].Id;
            this.selectedCityName1 = this.model.Cities[0].Name;
        }
    };
    ProfileComponent.prototype.cancelEditAddress = function () {
        this.model = JSON.parse(JSON.stringify(this.copymodel));
        this.editAddress();
    };
    ProfileComponent.prototype.cancelEditDescription = function () {
        this.model = JSON.parse(JSON.stringify(this.copymodel));
        this.editDescription();
    };
    ProfileComponent.prototype.cancelEditStudii = function () {
        this.model = JSON.parse(JSON.stringify(this.copymodel));
        this.editStudii();
    };
    ProfileComponent.prototype.cancelEditPrice = function () {
        this.model = JSON.parse(JSON.stringify(this.copymodel));
        this.editPrice();
    };
    ProfileComponent.prototype.cancelAva = function () {
        this.model = JSON.parse(JSON.stringify(this.copymodel));
        this.editAva();
    };
    ProfileComponent.prototype.saveCategories = function () {
        this.profileService.saveCategories(this.categories.filter(function (element) { return element.selected; })).subscribe(function (result) {
            console.log("Saved");
        });
        console.log(this.categories);
    };
    ProfileComponent.prototype.saveCycles = function () {
        this.profileService.saveCycles(this.cycles.filter(function (element) { return element.selected; })).subscribe(function (result) {
            console.log("Saved");
        });
        console.log(this.cycles);
    };
    ProfileComponent.prototype.open = function (content, options) {
        var _this = this;
        this.modalService.open(content, options).result.then(function (result) {
            _this.closeResult = "Closed with: " + result;
        }, function (reason) {
            _this.closeResult = "Dismissed " + _this.getDismissReason(reason);
        });
    };
    ProfileComponent.prototype.getDismissReason = function (reason) {
        if (reason === ng_bootstrap_1.ModalDismissReasons.ESC) {
            return 'by pressing ESC';
        }
        else if (reason === ng_bootstrap_1.ModalDismissReasons.BACKDROP_CLICK) {
            return 'by clicking on a backdrop';
        }
        else {
            return "with: " + reason;
        }
    };
    ProfileComponent.prototype.fileChangeListener = function (event) {
        var image = new Image();
        var file = event.target.files[0];
        var myReader = new FileReader();
        var that = this;
        myReader.onloadend = function (loadEvent) {
            image.src = loadEvent.target.result;
            that.cropper.setImage(image);
        };
        myReader.readAsDataURL(file);
    };
    ProfileComponent.prototype.saveImage = function () {
        var _this = this;
        this.profileService.saveProfileImage(this.data).subscribe(function (result) {
            console.log("Saved image");
            _this.profileService.getCurrentProfile().subscribe(function (results) {
                _this.model = results;
                _this.model.ProfileImageUrl += '?random+\=' + Math.random(); //force reload
                console.log("Profile reloaded");
            });
        });
    };
    ProfileComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            templateUrl: 'html/profile.component.html',
            providers: [cycle_service_1.CycleService, category_service_1.CategoryService, profile_service_1.ProfileService, ng2_img_cropper_1.ImageCropperComponent, { provide: ng_bootstrap_2.NgbDateAdapter, useClass: ng_bootstrap_2.NgbDateNativeAdapter }],
            selector: 'ngbd-datepicker-popup'
        }),
        __metadata("design:paramtypes", [ng_bootstrap_2.NgbCalendar,
            profile_service_1.ProfileService,
            router_1.Router,
            router_1.ActivatedRoute,
            ng_bootstrap_1.NgbModal,
            category_service_1.CategoryService,
            cycle_service_1.CycleService])
    ], ProfileComponent);
    return ProfileComponent;
}());
exports.ProfileComponent = ProfileComponent;
//# sourceMappingURL=profile.component.js.map