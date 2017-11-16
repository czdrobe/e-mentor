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
var pager_service_1 = require("../../services/pager.service");
var MessagesComponent = (function () {
    function MessagesComponent(messagesService, router, activateRoute, pagerService) {
        this.messagesService = messagesService;
        this.router = router;
        this.activateRoute = activateRoute;
        this.pagerService = pagerService;
        this.pager = {};
    }
    MessagesComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.activateRoute.queryParams.subscribe(function (params) {
            _this.currentpage = (params.hasOwnProperty("page") ? parseInt(params.page) : 1);
            _this.setPage(_this.currentpage);
        });
    };
    MessagesComponent.prototype.updateUrl = function () {
        //this.router.navigate(['/Teacher'], { queryParams: { maincategory: this.selectedMainCategory, category: this.selectedCategory, cycle: this.selectedCycle, page: this.currentpage } });
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
        this.messagesService.getMessages(page).subscribe(function (messagesResult) {
            console.log(messagesResult);
            // get pager object from service
            _this.pager = _this.pagerService.getPager(messagesResult.TotalRows, page);
            _this.messages = messagesResult.Entities;
        });
    };
    return MessagesComponent;
}());
MessagesComponent = __decorate([
    core_1.Component({
        moduleId: module.id,
        templateUrl: 'messages.component.html',
        providers: [pager_service_1.PagerService, messages_service_1.MessagesService]
    }),
    __metadata("design:paramtypes", [messages_service_1.MessagesService,
        router_1.Router,
        router_1.ActivatedRoute,
        pager_service_1.PagerService])
], MessagesComponent);
exports.MessagesComponent = MessagesComponent;
//# sourceMappingURL=messages.component.js.map