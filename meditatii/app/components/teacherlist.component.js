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
var category_service_1 = require("../services/category.service");
var cycle_service_1 = require("../services/cycle.service");
var router_1 = require("@angular/router");
var pager_service_1 = require("../services/pager.service");
var TeacherlistComponent = (function () {
    function TeacherlistComponent(userService, categoryService, cycleService, router, activateRoute, pagerService) {
        this.userService = userService;
        this.categoryService = categoryService;
        this.cycleService = cycleService;
        this.router = router;
        this.activateRoute = activateRoute;
        this.pagerService = pagerService;
        // pager object
        this.pager = {};
    }
    TeacherlistComponent.prototype.ngOnInit = function () {
        var _this = this;
        // load main categories
        this.categoryService.getMainCategories().subscribe(function (cats) {
            console.log(cats);
            _this.categories = cats;
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
        // load cycles
        this.cycleService.getCycles().subscribe(function (cycles) {
            console.log(cycles);
            _this.cycles = cycles;
        });
    };
    TeacherlistComponent.prototype.updateUrl = function () {
        this.router.navigate(['/Teacher'], { queryParams: { maincategory: this.selectedMainCategory, category: this.selectedCategory, cycle: this.selectedCycle, page: this.currentpage } });
    };
    TeacherlistComponent.prototype.selectMainCategory = function (id) {
        var _this = this;
        console.log('main');
        this.selectedMainCategory = id;
        this.categoryService.getSubCategories(id).subscribe(function (cats) {
            //console.log('subCategories:' + cats);
            _this.subCategories = cats;
            _this.updateUrl();
        });
    };
    TeacherlistComponent.prototype.selectCategory = function (id) {
        //console.log('categories');
        this.selectedCategory = id;
        this.currentpage = 1;
        this.updateUrl();
    };
    TeacherlistComponent.prototype.selectCycle = function (id) {
        this.selectedCycle = id;
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
            // get pager object from service
            _this.pager = _this.pagerService.getPager(usersResult.TotalRows, page);
            _this.teachers = usersResult.Entities;
        });
    };
    return TeacherlistComponent;
}());
TeacherlistComponent = __decorate([
    core_1.Component({
        moduleId: module.id,
        selector: 'teacherlist',
        templateUrl: 'teacherlist.component.html',
        providers: [users_service_1.UsersService, category_service_1.CategoryService, cycle_service_1.CycleService, pager_service_1.PagerService]
    }),
    __metadata("design:paramtypes", [users_service_1.UsersService,
        category_service_1.CategoryService,
        cycle_service_1.CycleService,
        router_1.Router,
        router_1.ActivatedRoute,
        pager_service_1.PagerService])
], TeacherlistComponent);
exports.TeacherlistComponent = TeacherlistComponent;
//# sourceMappingURL=teacherlist.component.js.map