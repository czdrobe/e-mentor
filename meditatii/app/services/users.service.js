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
var UsersService = /** @class */ (function () {
    function UsersService(http) {
        this.http = http;
        console.log('UserService initialized...');
    }
    UsersService.prototype.getUsers = function (categoryid, cycleid, page) {
        return this.http.get('/api/users/GetUsers?categoryid=' + (categoryid == null ? '0' : categoryid) + '&cycleid=' + (cycleid == null ? '0' : cycleid) + '&page=' + page).pipe(operators_1.map(function (res) { return res; }));
    };
    UsersService.prototype.getUser = function (userid) {
        return this.http.get('/api/users/GetUser?userid=' + (userid == null ? '0' : userid)).pipe(operators_1.map(function (res) { return res; }));
    };
    UsersService.prototype.getUserAvaiabilityForDay = function (userid, day) {
        return this.http.get('/api/users/getavailabilityforday?userid=' + (userid == null ? '0' : userid + '&day=' + day)).pipe(operators_1.map(function (res) { return res; }));
    };
    UsersService.prototype.saveAppoitment = function (teacherid, selecteddate, time) {
        return this.http.post('/api/users/saveappoitment?teacherId=' + (teacherid == null ? '0' : teacherid + '&selectedDate=' + selecteddate + '&startTime=' + time), null);
    };
    UsersService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], UsersService);
    return UsersService;
}());
exports.UsersService = UsersService;
//# sourceMappingURL=users.service.js.map