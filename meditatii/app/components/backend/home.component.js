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
var UserHomeComponent = /** @class */ (function () {
    function UserHomeComponent(calendar, profileService, router, activateRoute, modalService, categoryService, cycleService) {
        this.calendar = calendar;
        this.profileService = profileService;
        this.router = router;
        this.activateRoute = activateRoute;
        this.modalService = modalService;
        this.categoryService = categoryService;
        this.cycleService = cycleService;
        this.lstDuration = [
            { id: 60, name: 'o ora' },
            { id: 90, name: 'o ora si 30 de minute' },
            { id: 120, name: 'doua ore' },
            { id: 150, name: 'doua ore si 30 de minute' },
            { id: 180, name: 'trei ore' },
        ];
    }
    UserHomeComponent.prototype.changed = function () {
    };
    UserHomeComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.profileService.getAdsForCurrentUser().subscribe(function (results) {
            _this.lstAds = results;
        });
        this.categoryService.getCategoriesGroupped().subscribe(function (cats) {
            console.log(cats);
            _this.lstCategory = cats;
        });
        // load cycles
        this.cycleService.getCycles().subscribe(function (cycles) {
            console.log(cycles);
            _this.cycles = cycles;
        });
    };
    UserHomeComponent.prototype.open = function (content, options) {
        var _this = this;
        this.modalRef = this.modalService.open(content);
        this.modalRef.result.then(function (result) {
            _this.closeResult = "Closed with: " + result;
        }, function (reasonnew) {
            _this.closeResult = "Dismissed " + _this.getDismissReason(reasonnew);
        });
    };
    UserHomeComponent.prototype.getDismissReason = function (reason) {
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
    UserHomeComponent.prototype.newAd = function () {
        this.error = "";
        this.editedAdId = "";
        this.selectedCategoryId = 0;
        this.selectedCategoryName = "";
        this.price = 0;
        this.newMessage = "";
        this.selectedDurationId = 60;
        this.selectedDurationName = "";
        this.cycles.forEach(function (element) {
            element.selected = false;
        });
    };
    UserHomeComponent.prototype.deleteAd = function (code) {
        var _this = this;
        if (confirm("Esti sigur ca vrei sa stergi acest anunt ?")) {
            var newRequest = {
                Code: code,
            };
            this.profileService.deleteAd(newRequest).subscribe(function (result) {
                console.log("Deleted");
                _this.profileService.getAdsForCurrentUser().subscribe(function (results) {
                    _this.lstAds = results;
                });
            });
        }
    };
    UserHomeComponent.prototype.editAd = function (code) {
        this.error = "";
        this.editedAdId = code;
        var ads = this.lstAds.filter(function (el) {
            return el.Code === code;
        });
        var ad = ads[0];
        this.selectedCategoryId = ad.Category.Id;
        this.selectedCategoryName = ad.Category.Name;
        var durations = this.lstDuration.filter(function (el) {
            return el.id === ad.Duration;
        });
        var duration = durations[0];
        this.price = ad.Price;
        this.added = ad.Added;
        this.newMessage = ad.Description;
        this.selectedDurationId = duration.id;
        this.selectedDurationName = duration.name;
        //cycle
        this.cycles.forEach(function (element) {
            var foundElements = ad.Cycles.filter(function (x) { return x.Id == element.Id; });
            element.selected = foundElements.length > 0;
        });
    };
    UserHomeComponent.prototype.saveAd = function () {
        var _this = this;
        console.log("save Ad");
        var adCycles = [];
        this.cycles.forEach(function (cycle) {
            if (cycle.selected) {
                adCycles.push(cycle);
            }
        });
        this.error = "";
        //validate		
        if (this.selectedCategoryId <= 0) {
            this.error = "Selecteaza o materie!";
        }
        else if (this.newMessage == "") {
            this.error = "Adauga o descriere!";
        }
        else if (adCycles.length <= 0) {
            this.error = "Selecteaza cel putin 1 nivel!";
        }
        if (this.error == "") {
            var newRequest = {
                Code: this.editedAdId,
                Category: { Id: this.selectedCategoryId },
                Duration: this.selectedDurationId,
                Price: this.price,
                Added: this.added,
                Description: this.newMessage,
                Cycles: adCycles
            };
            var localModalRef_1 = this.modalRef;
            this.profileService.saveAd(newRequest).subscribe(function (result) {
                console.log("Saved");
                _this.profileService.getAdsForCurrentUser().subscribe(function (results) {
                    _this.lstAds = results;
                });
                localModalRef_1.close();
            });
        }
    };
    UserHomeComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            templateUrl: 'html/home.component.html',
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
    ], UserHomeComponent);
    return UserHomeComponent;
}());
exports.UserHomeComponent = UserHomeComponent;
//# sourceMappingURL=home.component.js.map