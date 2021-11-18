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
var MessagesService = (function () {
    function MessagesService(http) {
        this.http = http;
        console.log('MessagesService initialized...');
    }
    MessagesService.prototype.getMessages = function (mentorId, page) {
        return this.http.get('/api/messages/getmessages/' + mentorId + '/' + page).pipe(operators_1.map(function (res) { return res; }));
    };
    MessagesService.prototype.getMentors = function () {
        return this.http.get('api/messages/listofmentors').pipe(operators_1.map(function (res) { return res; }));
    };
    MessagesService.prototype.getUsers = function () {
        return this.http.get('api/messages/getlistofuserswithmessage').pipe(operators_1.map(function (res) { return res; }));
    };
    MessagesService.prototype.saveMessage = function (toId, body) {
        var formData = new FormData();
        formData.append('ToUserId', toId.toString());
        formData.append('Body', body);
        return this.http.post('api/messages/SaveNewMessage', { ToUserId: toId.toString(), Body: body }, httpOptions);
    };
    MessagesService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], MessagesService);
    return MessagesService;
}());
exports.MessagesService = MessagesService;
