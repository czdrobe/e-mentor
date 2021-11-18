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
var users_service_1 = require("../services/users.service");
var router_1 = require("@angular/router");
var category_service_1 = require("../services/category.service");
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var cycle_service_1 = require("../services/cycle.service");
var profile_service_1 = require("../services/profile.service");
var pager_service_1 = require("../services/pager.service");
var RequestnewComponent = /** @class */ (function () {
    function RequestnewComponent(categoryService, userService, modalService, activateRoute, pagerService, router, profileService, cycleService) {
        this.categoryService = categoryService;
        this.userService = userService;
        this.modalService = modalService;
        this.activateRoute = activateRoute;
        this.pagerService = pagerService;
        this.router = router;
        this.profileService = profileService;
        this.cycleService = cycleService;
    }
    RequestnewComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.isOnline = false;
        this.selectedCategoryName = "Alege o materie";
        this.selectedCycleName = "Alege ciclul scolar";
        this.selectedCityName = "Alege o localitate";
        this.profileService.getCurrentProfile().subscribe(function (results) {
            console.log(results);
            _this.currentUser = results;
            if (_this.currentUser == null) {
                _this.router.navigate(['/Account/Login'], { queryParams: {} }).then(function () {
                    window.location.reload();
                });
            }
            else {
                _this.categoryService.getallwithsubcategories().subscribe(function (cats) {
                    _this.categories = cats;
                });
                // load cycles
                _this.cycleService.getCycles().subscribe(function (cycles) {
                    console.log(cycles);
                    _this.cycles = cycles;
                });
                // load cities
                _this.profileService.getCities().subscribe(function (cities) {
                    console.log(cities);
                    _this.cities = cities;
                });
            }
        });
    };
    RequestnewComponent.prototype.selectCategory = function (id, categorytName) {
        this.selectedCategory = id;
        this.selectedCategoryName = categorytName;
    };
    RequestnewComponent.prototype.selectCity = function (id, cityName) {
        this.selectedCity = id;
        this.selectedCityName = cityName;
    };
    RequestnewComponent.prototype.saveNewRequest = function () {
        var isvalid = true;
        this.errorMessage = "";
        if (this.selectedCategory === undefined || this.selectedCategory == null || this.selectedCategory <= 0) {
            this.errorMessage = "Alege o materie";
            isvalid = false;
        }
        else if (!this.isOnline && (this.selectedCity === undefined || this.selectedCity == null || this.selectedCity <= 0)) {
            this.errorMessage = "Alege un oras";
            isvalid = false;
        }
        else if (this.newMessage === undefined || this.newMessage == "") {
            this.errorMessage = "Te rog introdu ce anume cauți ?";
            isvalid = false;
        }
        else if (this.duration === undefined || this.duration < 0) {
            this.errorMessage = "Alege durata solicitarii";
            isvalid = false;
        }
        else if (this.selectedCycle === undefined || this.selectedCycle == null || this.selectedCycle <= 0) {
            this.errorMessage = "Alege categoria de varsta";
            isvalid = false;
        }
        if (isvalid) {
            var newRequest = {
                CategoryId: this.selectedCategory,
                IsOnline: this.isOnline,
                CityId: this.selectedCity,
                Description: this.newMessage,
                CycleId: this.selectedCycle,
                Duration: this.duration
                //duration ?
            };
            this.userService.saveRequest(newRequest).subscribe(function (result) {
                console.log("Saved");
                window.location.href = "/solicitare-de-meditatii/" + result;
            });
        }
    };
    RequestnewComponent.prototype.open = function (content, options) {
        var _this = this;
        this.modalService.open(content, options).result.then(function (result) {
            _this.closeResult = "Closed with: " + result;
        }, function (reason) {
            _this.closeResult = "Dismissed " + _this.getDismissReason(reason);
        });
    };
    RequestnewComponent.prototype.getDismissReason = function (reason) {
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
    RequestnewComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: 'requestnew',
            templateUrl: 'html/requestnew.component.html',
            providers: [users_service_1.UsersService, pager_service_1.PagerService, cycle_service_1.CycleService, category_service_1.CategoryService, profile_service_1.ProfileService]
        }),
        __metadata("design:paramtypes", [category_service_1.CategoryService,
            users_service_1.UsersService,
            ng_bootstrap_1.NgbModal,
            router_1.ActivatedRoute,
            pager_service_1.PagerService,
            router_1.Router,
            profile_service_1.ProfileService,
            cycle_service_1.CycleService])
    ], RequestnewComponent);
    return RequestnewComponent;
}());
exports.RequestnewComponent = RequestnewComponent;
//# sourceMappingURL=requestnew.component.js.map