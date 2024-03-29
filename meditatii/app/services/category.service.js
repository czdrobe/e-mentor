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
var operators_1 = require("rxjs/operators");
var http_1 = require("@angular/common/http");
var CategoryService = /** @class */ (function () {
    function CategoryService(http) {
        this.http = http;
        console.log('CategoryService initialized...');
    }
    CategoryService.prototype.getCategories = function () {
        return this.http.get('/api/categories/getall').pipe(operators_1.map(function (res) { return res; }));
    };
    CategoryService.prototype.getCategoriesGroupped = function () {
        return this.http.get('/api/categories/getallgroupped').pipe(operators_1.map(function (res) { return res; }));
    };
    CategoryService.prototype.getMainCategories = function () {
        return this.http.get('/api/categories/getmains').pipe(operators_1.map(function (res) { return res; }));
    };
    CategoryService.prototype.getallwithsubcategories = function () {
        return this.http.get('/api/categories/getallwithsubcategories').pipe(operators_1.map(function (res) { return res; }));
    };
    CategoryService.prototype.getSubCategories = function (id) {
        return this.http.get('/api/categories/getsubcategories?id=' + id).pipe(operators_1.map(function (res) { return res; }));
    };
    CategoryService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], CategoryService);
    return CategoryService;
}());
exports.CategoryService = CategoryService;
//# sourceMappingURL=category.service.js.map