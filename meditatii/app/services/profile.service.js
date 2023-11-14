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
var rxjs_1 = require("rxjs");
var httpOptions = {
    headers: new http_1.HttpHeaders({ 'Content-Type': 'application/json' })
};
var httpOptionsMulti = {
    headers: new http_1.HttpHeaders({ 'Content-Type': 'multipart/form-data' })
};
var ProfileService = /** @class */ (function () {
    function ProfileService(http) {
        this.http = http;
        console.log('ProfileService initialized...');
    }
    ProfileService.prototype.getCurrentProfile = function () {
        return this.http.get('/api/users/GetCurrentProfile').pipe(operators_1.map(function (res) { return res; }));
    };
    ProfileService.prototype.getAvailability = function () {
        return this.http.get('/api/users/getavailability').pipe(operators_1.map(function (res) { return res; }));
    };
    ProfileService.prototype.saveCurrentProfie = function (profile) {
        //return this.http.post('api/users/SaveCurrentProfile', profile, httpOptions);
        return this.http.post('api/users/SaveCurrentProfile', profile, httpOptions);
    };
    ProfileService.prototype.saveAvaiability = function (lstAvaibility) {
        return this.http.post('/api/users/saveavailability', lstAvaibility, httpOptions);
    };
    ProfileService.prototype.saveCategories = function (lstCategories) {
        return this.http.post('/api/users/savecategories', lstCategories, httpOptions);
    };
    ProfileService.prototype.saveCycles = function (lstCycles) {
        return this.http.post('/api/users/savecycles', lstCycles, httpOptions);
    };
    ProfileService.prototype.saveCityForCurrentProfie = function (city1, atTeacher, atStudent, isOnline) {
        return this.http.post('/api/users/savecityforcurrentprofie', { cityid1: city1, atTeacher: atTeacher, atStudent: atStudent, isOnline: isOnline }, httpOptions);
    };
    ProfileService.prototype.saveProfileImage = function (image) {
        var formData = new FormData();
        formData.append('profileimage', image.image);
        return this.http.post('/api/users/saveprofileimage', formData);
    };
    ProfileService.prototype.getCities = function () {
        return this.http.get('/api/users/GetCities').pipe(operators_1.map(function (res) { return res; }));
    };
    ProfileService.prototype.getExperience = function () {
        console.log('start inExperience');
        return this.http.get('/api/users/GetExperience').pipe(operators_1.map(function (res) { return res; }));
    };
    ProfileService.prototype.getOccupation = function () {
        console.log('start inOccupation');
        return this.http.get('/api/users/GetOccupation').pipe(operators_1.map(function (res) { return res; }));
    };
    ProfileService.prototype.getAdsForCurrentUser = function () {
        return this.http.get('/api/users/getadsforcurrentuser').pipe(operators_1.map(function (res) { return res; }));
    };
    ProfileService.prototype.saveAd = function (adrequest) {
        return this.http.post('/api/users/savead', adrequest, httpOptions);
    };
    ProfileService.prototype.deleteAd = function (adrequest) {
        return this.http.post('/api/users/deletead', adrequest, httpOptions);
    };
    ProfileService.prototype.handleError = function (error) {
        if (error.error instanceof ErrorEvent) {
            // A client-side or network error occurred. Handle it accordingly.
            console.error('An error occurred:', error.error.message);
        }
        else {
            // The backend returned an unsuccessful response code.
            // The response body may contain clues as to what went wrong,
            console.error("Backend returned code " + error.status + ", " +
                ("body was: " + error.error));
        }
        // return an observable with a user-facing error message
        return rxjs_1.throwError('Something bad happened; please try again later.');
    };
    ;
    ProfileService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], ProfileService);
    return ProfileService;
}());
exports.ProfileService = ProfileService;
//# sourceMappingURL=profile.service.js.map