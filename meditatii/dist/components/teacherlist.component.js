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
var TeacherlistComponent = (function () {
    function TeacherlistComponent(userService, categoryService, cycleService, router, activateRoute, pagerService, messagesService, modalService, profileService) {
        this.userService = userService;
        this.categoryService = categoryService;
        this.cycleService = cycleService;
        this.router = router;
        this.activateRoute = activateRoute;
        this.pagerService = pagerService;
        this.messagesService = messagesService;
        this.modalService = modalService;
        this.profileService = profileService;
        this.pager = {};
    }
    TeacherlistComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.selectedCategoryName = "Alege o materie";
        this.selectedCycleName = "Alege ciclul scolar";
        this.profileService.getCurrentProfile().subscribe(function (profile) {
            _this.isCurrentUserIsLoggedIn = profile != null;
            _this.categoryService.getallwithsubcategories().subscribe(function (cats) {
                console.log(cats);
                _this.categories = cats;
                _this.searchMaterii = "";
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
                    _this.currentpage = (params.hasOwnProperty("page") ? parseInt(params.page) : 1);
                    _this.setPage(_this.currentpage);
                });
            });
        });
        this.cycleService.getCycles().subscribe(function (cycles) {
            console.log(cycles);
            _this.cycles = cycles;
        });
    };
    TeacherlistComponent.prototype.updateUrl = function () {
        this.router.navigate(['/teacher'], { queryParams: { maincategory: this.selectedMainCategory, category: this.selectedCategory, cycle: this.selectedCycle, page: this.currentpage } });
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
        this.selectedCategory = id;
        this.selectedCategoryName = categorytName;
        this.currentpage = 1;
        this.updateUrl();
    };
    TeacherlistComponent.prototype.selectCycle = function (id, cycleName) {
        this.selectedCycle = id;
        this.selectedCycleName = cycleName;
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
        this.userService.getUsers(this.selectedCategory, this.selectedCycle, page).subscribe(function (usersResult) {
            console.log(usersResult);
            _this.pager = _this.pagerService.getPager(usersResult.TotalRows, page);
            _this.teachers = usersResult.Entities;
        });
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
            profile_service_1.ProfileService])
    ], TeacherlistComponent);
    return TeacherlistComponent;
}());
exports.TeacherlistComponent = TeacherlistComponent;
