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
var messages_service_1 = require("../../services/messages.service");
var router_1 = require("@angular/router");
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var rxjs_1 = require("rxjs");
var operators_1 = require("rxjs/operators");
var pager_service_1 = require("../../services/pager.service");
var messageModel_1 = require("../../models/messageModel");
var MessagesComponent = (function () {
    function MessagesComponent(messagesService, router, activateRoute, pagerService, modalService) {
        this.messagesService = messagesService;
        this.router = router;
        this.activateRoute = activateRoute;
        this.pagerService = pagerService;
        this.modalService = modalService;
        this.pager = {};
        this._success = new rxjs_1.Subject();
        this.model = new messageModel_1.MessageModel("");
    }
    Object.defineProperty(MessagesComponent.prototype, "diagnostic", {
        get: function () { return JSON.stringify(this.model); },
        enumerable: true,
        configurable: true
    });
    MessagesComponent.prototype.sendMessage = function () {
        var _this = this;
        this.messagesService.saveMessage(this.currentMentorId, this.model.newMessage).subscribe(function (result) {
            _this.model.newMessage = "";
            _this.currentpage = 1;
            _this.setPage(_this.currentpage);
            _this._success.next("Mesajul a fost trimis");
        });
        console.log('newmessage value:' + this.model.newMessage);
    };
    MessagesComponent.prototype.ngOnInit = function () {
        var _this = this;
        this._success.subscribe(function (message) { return _this.successMessage = message; });
        this._success.pipe(operators_1.debounceTime(5000)).subscribe(function () { return _this.successMessage = null; });
        this.activateRoute.queryParams.subscribe(function (params) {
            _this.messagesService.getUsers().subscribe(function (results) {
                console.log(results);
                _this.mentors = results;
                console.log(_this.mentors);
            });
        });
    };
    MessagesComponent.prototype.updateUrl = function () {
    };
    MessagesComponent.prototype.LoadMessages = function (mentorMessage) {
        this.currentpage = 1;
        this.currentMentorId = mentorMessage.Id;
        this.setPage(this.currentpage);
    };
    MessagesComponent.prototype.setCurrentPage = function (page) {
        this.currentpage = page;
        this.updateUrl();
    };
    MessagesComponent.prototype.setPage = function (page) {
        var _this = this;
        if (page < 1) {
            return;
        }
        this.currentpage = page;
        this.messagesService.getMessages(this.currentMentorId, page).subscribe(function (messagesResult) {
            console.log(messagesResult);
            _this.pager = _this.pagerService.getPager(messagesResult.TotalRows, page);
            _this.messages = messagesResult.Entities;
        });
    };
    MessagesComponent.prototype.open = function (content, options) {
        var _this = this;
        this.modalService.open(content, options).result.then(function (result) {
            _this.closeResult = "Closed with: " + result;
        }, function (reason) {
            _this.closeResult = "Dismissed " + _this.getDismissReason(reason);
        });
    };
    MessagesComponent.prototype.getDismissReason = function (reason) {
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
    MessagesComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            templateUrl: 'html/messages.component.html',
            providers: [pager_service_1.PagerService, messages_service_1.MessagesService]
        }),
        __metadata("design:paramtypes", [messages_service_1.MessagesService,
            router_1.Router,
            router_1.ActivatedRoute,
            pager_service_1.PagerService,
            ng_bootstrap_1.NgbModal])
    ], MessagesComponent);
    return MessagesComponent;
}());
exports.MessagesComponent = MessagesComponent;
