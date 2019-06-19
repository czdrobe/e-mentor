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
    }
    ProfileComponent.prototype.changed = function () {
    };
    ProfileComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.mytime = new Date();
        this.ismeridian = true;
        this.profileService.getCurrentProfile().subscribe(function (results) {
            _this.model = results;
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
        });
        this.profileService.getAvailability().subscribe(function (results) {
            _this.availability = [];
            var tempAvailability = [];
            var startTime = 0;
            var endTime = 0;
            var day = 0;
            var totalElements = results.Entities.length;
            var i = 0;
            results.Entities.forEach(function (element) {
                i++;
                if (day == 0) {
                    day = element.Day;
                    startTime = element.Time;
                }
                if (day != element.Day || i == totalElements) //create also for last element
                 {
                    if (i == totalElements) {
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
            for (var j = 1; j <= 7; j++) {
                var availability = tempAvailability.filter(function (i) { return i.day == j; })[0];
                var dayName = "Luni";
                switch (j) {
                    case 1:
                        dayName = "Luni";
                        break;
                    case 2:
                        dayName = "Marti";
                        break;
                    case 3:
                        dayName = "Miercuri";
                        break;
                    case 4:
                        dayName = "Joi";
                        break;
                    case 5:
                        dayName = "Vineri";
                        break;
                    case 6:
                        dayName = "Sambata";
                        break;
                    case 7:
                        dayName = "Duminica";
                        break;
                }
                _this.availability.push({
                    day: j,
                    dayName: dayName,
                    startTime: (availability != null ? availability.startTime : 0),
                    endTime: (availability != null ? availability.endTime : 0),
                    available: (availability != null && availability.startTime > 0 && availability.endTime) ? 1 : 0
                });
            }
        });
    };
    ProfileComponent.prototype.saveProfile = function () {
        this.saveProfileToDB();
        this.editProfile();
    };
    ProfileComponent.prototype.saveAddress = function () {
        this.saveProfileToDB();
        this.editAddress();
    };
    ProfileComponent.prototype.saveDescription = function () {
        this.saveProfileToDB();
        this.editDescription();
    };
    ProfileComponent.prototype.savePrice = function () {
        this.saveProfileToDB();
        this.editPrice();
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
    ProfileComponent.prototype.saveProfileToDB = function () {
        if (this.model.Dob != null) {
            var dob = new Date(Date.UTC(this.model.Dob.getFullYear(), this.model.Dob.getMonth(), this.model.Dob.getDate()));
            this.model.Dob = dob;
        }
        this.profileService.saveCurrentProfie(this.model).subscribe(function (result) {
            console.log("Saved");
        });
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
    ProfileComponent.prototype.cancelEditProfile = function () {
        this.model = JSON.parse(JSON.stringify(this.copymodel));
        this.editProfile();
    };
    ProfileComponent.prototype.cancelEditAddress = function () {
        this.model = JSON.parse(JSON.stringify(this.copymodel));
        this.editAddress();
    };
    ProfileComponent.prototype.cancelEditDescription = function () {
        this.model = JSON.parse(JSON.stringify(this.copymodel));
        this.editDescription();
    };
    ProfileComponent.prototype.cancelEditPrice = function () {
        this.model = JSON.parse(JSON.stringify(this.copymodel));
        this.editDescription();
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
            templateUrl: 'profile.component.html',
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