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
var profile_service_1 = require("../services/profile.service");
var category_service_1 = require("../services/category.service");
var cycle_service_1 = require("../services/cycle.service");
var router_1 = require("@angular/router");
var messages_service_1 = require("../services/messages.service");
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var pager_service_1 = require("../services/pager.service");
var TeacherlistComponent = /** @class */ (function () {
    function TeacherlistComponent(userService, categoryService, cycleService, router, activateRoute, pagerService, messagesService, modalService, profileService, 
    //private ddService: NgbDropdown,
    rootElement) {
        this.userService = userService;
        this.categoryService = categoryService;
        this.cycleService = cycleService;
        this.router = router;
        this.activateRoute = activateRoute;
        this.pagerService = pagerService;
        this.messagesService = messagesService;
        this.modalService = modalService;
        this.profileService = profileService;
        // pager object
        this.pager = {};
        this.selectedUrlCategory = "";
        this.selectedUrlCategoryName = "";
        var domElementCat = document.getElementById("catId");
        if (domElementCat != null) {
            this.selectedUrlCategory = domElementCat.getAttribute("value");
            this.selectedCategory = Number(this.selectedUrlCategory);
        }
        var domElementCatName = document.getElementById("catName");
        if (domElementCatName != null) {
            this.selectedUrlCategoryName = domElementCatName.getAttribute("value");
        }
        this.orders = [{ Id: 1, Name: 'Cele mai populare' }, { Id: 2, Name: 'Review-uri' }, { Id: 3, Name: 'Cele mai noi' }];
    }
    TeacherlistComponent.prototype.ngOnInit = function () {
        var _this = this;
        console.log(this.selectedUrlCategory);
        console.log(this.selectedUrlCategoryName);
        this.selectedCategoryName = "Alege o materie";
        this.selectedCycleName = "Alege ciclul scolar";
        this.selectedOrderName = "Cele mai populare";
        this.selectedCityName = "Alege o localitate";
        this.selectedOrder = 1;
        // load main categories
        this.profileService.getCurrentProfile().subscribe(function (profile) {
            _this.isCurrentUserIsLoggedIn = profile != null;
            _this.categoryService.getallwithsubcategories().subscribe(function (cats) {
                console.log(cats);
                _this.categories = cats;
                _this.searchMaterii = ""; //in fact we reset the search
                _this.searchCity = "";
                _this.activateRoute.params.subscribe(function (params) {
                    if (params.hasOwnProperty("maincategory")) {
                    }
                });
                _this.activateRoute.queryParams.subscribe(function (params) {
                    if (params.hasOwnProperty("maincategory")) {
                        _this.selectedMainCategory = params.maincategory;
                        _this.selectMainCategory(_this.selectedMainCategory);
                    }
                    if (params.hasOwnProperty("category")) {
                        _this.selectedCategory = params.category;
                    }
                    if (params.hasOwnProperty("cycle")) {
                        _this.selectedCycle = params.cycle;
                    }
                    if (params.hasOwnProperty("city")) {
                        _this.selectedCity = params.city;
                    }
                    if (params.hasOwnProperty("order")) {
                        _this.selectedOrder = params.order;
                    }
                    _this.currentpage = (params.hasOwnProperty("page") ? parseInt(params.page) : 1);
                    _this.setPage(_this.currentpage);
                });
            });
        });
        this.categoryService.getCategoriesGroupped().subscribe(function (cats) {
            console.log(cats);
            _this.lstCategoryGroupped = cats;
        });
        // load cycles
        this.cycleService.getCycles().subscribe(function (cycles) {
            console.log(cycles);
            _this.cycles = cycles;
        });
        // load cities
        this.profileService.getCities().subscribe(function (cities) {
            console.log(cities);
            _this.cities = cities;
        });
    };
    TeacherlistComponent.prototype.unSelectCity = function () {
        this.selectedCity = null;
        this.selectedCityName = null;
        this.cities.forEach(function (element) {
            //console.log(element);
            element.selected = false;
        });
        this.updateUrl();
    };
    TeacherlistComponent.prototype.unSelectCycle = function () {
        this.selectedCycle = null;
        this.selectedCycleName = null;
        this.cycles.forEach(function (element) {
            element.selected = false;
        });
        this.updateUrl();
    };
    TeacherlistComponent.prototype.resetFilters = function () {
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
    };
    TeacherlistComponent.prototype.updateUrl = function () {
        this.router.navigate([], { queryParams: { cycle: this.selectedCycle, city: this.selectedCity, order: this.selectedOrder, page: this.currentpage } });
    };
    TeacherlistComponent.prototype.selectMainCategory = function (id) {
        var _this = this;
        console.log('main');
        this.selectedMainCategory = id;
        this.categoryService.getSubCategories(id).subscribe(function (cats) {
            _this.subCategories = cats;
            _this.updateUrl();
        });
    };
    TeacherlistComponent.prototype.selectCategory = function (id, categorytName) {
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
    };
    TeacherlistComponent.prototype.selectOrder = function (id, orderName) {
        this.selectedOrder = id;
        this.selectedOrderName = orderName;
        this.currentpage = 1;
        this.updateUrl();
    };
    TeacherlistComponent.prototype.selectCycle = function (id, cycleName) {
        this.selectedCycle = id;
        this.selectedCycleName = cycleName;
        this.currentpage = 1;
        this.updateUrl();
    };
    TeacherlistComponent.prototype.selectCity = function (city) {
        this.selectedCity = city.Id;
        this.selectedCityName = city.Name;
        city.selected = !city.selected;
        this.currentpage = 1;
        this.updateUrl();
    };
    TeacherlistComponent.prototype.setCurrentPage = function (page) {
        this.currentpage = page;
        this.updateUrl();
    };
    TeacherlistComponent.prototype.setPage = function (page) {
        var _this = this;
        if (page < 1) {
            return;
        }
        this.currentpage = page;
        this.userService.getAds(this.selectedCategory, this.selectedCycle, this.selectedCity, this.selectedOrder, page).subscribe(function (usersResult) {
            console.log(usersResult);
            // get pager object from service
            _this.pager = _this.pagerService.getPager(usersResult.TotalRows, page);
            _this.ads = usersResult.Entities;
            _this.nrOfTeachers = usersResult.TotalRows;
        });
        /*
        this.userService.getUsers(this.selectedCategory, this.selectedCycle, this.selectedCity, this.selectedOrder, page).subscribe((usersResult:any) => {
            console.log(usersResult);

            // get pager object from service
            this.pager = this.pagerService.getPager(usersResult.TotalRows, page);

            this.teachers = usersResult.Entities;

            this.nrOfTeachers = usersResult.TotalRows;
        });*/
    };
    TeacherlistComponent.prototype.selectUser = function (userId) {
        this.selectedUserId = userId;
    };
    TeacherlistComponent.prototype.open = function (content, options) {
        var _this = this;
        this.modalService.open(content, options).result.then(function (result) {
            _this.closeResult = "Closed with: " + result;
        }, function (reason) {
            _this.closeResult = "Dismissed " + _this.getDismissReason(reason);
        });
    };
    TeacherlistComponent.prototype.getDismissReason = function (reason) {
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
    TeacherlistComponent.prototype.sendMessage = function () {
        var _this = this;
        this.messagesService.saveMessage(this.selectedUserId, this.newMessage).subscribe(function (result) {
            _this.newMessage = "";
        });
        console.log('newmessage value:' + this.newMessage);
    };
    TeacherlistComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: 'teacherlist',
            templateUrl: 'html/teacherlist.component.html',
            providers: [users_service_1.UsersService, category_service_1.CategoryService, cycle_service_1.CycleService, pager_service_1.PagerService, messages_service_1.MessagesService, profile_service_1.ProfileService]
        }),
        __metadata("design:paramtypes", [users_service_1.UsersService,
            category_service_1.CategoryService,
            cycle_service_1.CycleService,
            router_1.Router,
            router_1.ActivatedRoute,
            pager_service_1.PagerService,
            messages_service_1.MessagesService,
            ng_bootstrap_1.NgbModal,
            profile_service_1.ProfileService,
            core_1.ElementRef])
    ], TeacherlistComponent);
    return TeacherlistComponent;
}());
exports.TeacherlistComponent = TeacherlistComponent;
//# sourceMappingURL=teacherlist.component.js.map