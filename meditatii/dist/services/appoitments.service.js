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
var AppoitmentsService = (function () {
    function AppoitmentsService(http) {
        this.http = http;
        console.log('MessagesService initialized...');
    }
    AppoitmentsService.prototype.getAppoitments = function (page) {
        return this.http.get('/api/appoitments/getappoitments/' + page).pipe(operators_1.map(function (res) { return res; }));
    };
    AppoitmentsService.prototype.getActiveAppoitments = function (page) {
        return this.http.get('/api/appoitments/getactiveappoitments/' + page).pipe(operators_1.map(function (res) { return res; }));
    };
    AppoitmentsService.prototype.getOldAppoitments = function (page) {
        return this.http.get('/api/appoitments/getoldappoitments/' + page).pipe(operators_1.map(function (res) { return res; }));
    };
    AppoitmentsService.prototype.saveAppoitment = function (teacherId, startDate, endDate) {
        return this.http.post('api/appoitments/SaveAppoitment', { TeacherId: teacherId.toString(), StartDate: startDate, EndDate: endDate }, httpOptions);
    };
    AppoitmentsService.prototype.getChatForAppoitment = function (appoitmentid) {
        return this.http.get('/api/appoitments/getchatforappoitment/' + appoitmentid).pipe(operators_1.map(function (res) { return res; }));
    };
    AppoitmentsService.prototype.deleteAppoitment = function (appoitmentid) {
        return this.http.get('api/appoitments/deleteappoitment/' + appoitmentid).pipe(operators_1.map(function (res) { return res; }));
    };
    AppoitmentsService.prototype.saveTeacherRating = function (appointmentid, rate) {
        return this.http.post('api/teacherrating/getallratingforteacher', { AppoitmentId: appointmentid.toString(), Rating: rate.toString() }, httpOptions);
    };
    AppoitmentsService.prototype.payment = function (listOfAppoitments) {
        var body = JSON.stringify(listOfAppoitments);
        return this.http.post('api/appoitments/savepayments', body, httpOptions);
    };
    AppoitmentsService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], AppoitmentsService);
    return AppoitmentsService;
}());
exports.AppoitmentsService = AppoitmentsService;
