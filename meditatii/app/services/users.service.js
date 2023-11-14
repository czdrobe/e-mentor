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
var httpOptions = {
    headers: new http_1.HttpHeaders({ 'Content-Type': 'application/json' })
};
var httpOptionsMulti = {
    headers: new http_1.HttpHeaders({ 'Content-Type': 'multipart/form-data' })
};
var UsersService = /** @class */ (function () {
    function UsersService(http) {
        this.http = http;
        console.log('UserService initialized...');
    }
    UsersService.prototype.getAds = function (categoryid, cycleid, cityid, order, page) {
        return this.http.get('/api/users/getusersdescription?categoryid=' + (categoryid == null ? '0' : categoryid) + '&cycleid=' + (cycleid == null ? '0' : cycleid) + '&cityid=' + (cityid == null ? '0' : cityid) + '&order=' + (order == null ? '0' : order) + '&page=' + page).pipe(operators_1.map(function (res) { return res; }));
    };
    UsersService.prototype.getUsers = function (categoryid, cycleid, cityid, order, page) {
        return this.http.get('/api/users/GetUsers?categoryid=' + (categoryid == null ? '0' : categoryid) + '&cycleid=' + (cycleid == null ? '0' : cycleid) + '&cityid=' + (cityid == null ? '0' : cityid) + '&order=' + (order == null ? '0' : order) + '&page=' + page).pipe(operators_1.map(function (res) { return res; }));
    };
    UsersService.prototype.getUser = function (userid) {
        return this.http.get('/api/users/GetUser?userid=' + (userid == null ? '0' : userid)).pipe(operators_1.map(function (res) { return res; }));
    };
    UsersService.prototype.adView = function (adid) {
        return this.http.get('/api/users/AdView?adCode=' + (adid == null ? '0' : adid)).pipe(operators_1.map(function (res) { return res; }));
    };
    UsersService.prototype.getNrOfViewsForAd = function (adid) {
        return this.http.get('/api/users/getNrOfViewsForAd?adCode=' + (adid == null ? '0' : adid)).pipe(operators_1.map(function (res) { return res; }));
    };
    UsersService.prototype.getAdByCode = function (adid) {
        return this.http.get('/api/users/getadbycode?adid=' + (adid == null ? '0' : adid)).pipe(operators_1.map(function (res) { return res; }));
    };
    UsersService.prototype.getUserByCode = function (userid) {
        return this.http.get('/api/users/getuserbycode?userid=' + (userid == null ? '0' : userid)).pipe(operators_1.map(function (res) { return res; }));
    };
    UsersService.prototype.getUserPhoneNumber = function (userid) {
        return this.http.get('/api/users/getUserPhoneNumber?userid=' + (userid == null ? '0' : userid)).pipe(operators_1.map(function (res) { return res; }));
    };
    UsersService.prototype.getUserAvaiabilityForDay = function (userid, day) {
        return this.http.get('/api/users/getavailabilityforday?userid=' + (userid == null ? '0' : userid + '&day=' + day)).pipe(operators_1.map(function (res) { return res; }));
    };
    UsersService.prototype.getRatingsForTeacher = function (userid) {
        return this.http.get('/api/teacherrating/getallratingforteacher?teacherid=' + (userid == null ? '0' : userid)).pipe(operators_1.map(function (res) { return res; }));
    };
    UsersService.prototype.getRecomandations = function (userid, categoryid, cityid) {
        return this.http.get('/api/users/GetSuggestedUsers?userid=' + userid + 'categoryid=' + categoryid + '&cityid=' + cityid).pipe(operators_1.map(function (res) { return res; }));
    };
    UsersService.prototype.saveAppoitment = function (teacherid, selecteddate, time) {
        return this.http.post('/api/users/saveappoitment?teacherId=' + (teacherid == null ? '0' : teacherid + '&selectedDate=' + selecteddate + '&startTime=' + time), null);
    };
    UsersService.prototype.getRequests = function (city, subject, page) {
        return this.http.get('/api/users/GetRequests?city=' + (city == null ? "" : city) + '&subject=' + (subject == null ? "" : subject) + "&page=" + page).pipe(operators_1.map(function (res) { return res; }));
    };
    UsersService.prototype.getRequest = function (id) {
        return this.http.get('/api/users/GetRequest?id=' + (id == null ? "" : id)).pipe(operators_1.map(function (res) { return res; }));
    };
    UsersService.prototype.saveRequest = function (newRequet) {
        //return this.http.post('api/users/SaveCurrentProfile', profile, httpOptions);
        return this.http.post('api/users/SaveNewRequest', newRequet, httpOptions);
    };
    UsersService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], UsersService);
    return UsersService;
}());
exports.UsersService = UsersService;
//# sourceMappingURL=users.service.js.map