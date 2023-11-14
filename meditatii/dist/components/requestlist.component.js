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
var router_1 = require("@angular/router");
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var pager_service_1 = require("../services/pager.service");
var RequestlistComponent = /** @class */ (function () {
    function RequestlistComponent(userService, modalService, activateRoute, pagerService, router) {
        this.userService = userService;
        this.modalService = modalService;
        this.activateRoute = activateRoute;
        this.pagerService = pagerService;
        this.router = router;
        this.pager = {};
    }
    RequestlistComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.activateRoute.queryParams.subscribe(function (params) {
            if (params.hasOwnProperty("city")) {
                _this.selectedCity = params.city;
            }
            if (params.hasOwnProperty("subject")) {
                _this.selectedSubject = params.subject;
            }
            _this.currentpage = (params.hasOwnProperty("page") ? parseInt(params.page) : 1);
            _this.setPage(_this.currentpage);
        });
    };
    RequestlistComponent.prototype.updateUrl = function () {
        this.router.navigate(['/solicitare-de-meditatii'], { queryParams: { subject: this.selectedSubject, city: this.selectedCity, page: this.currentpage } });
    };
    RequestlistComponent.prototype.new = function () {
        this.router.navigate(['/adauga-solicitare-noua'], { queryParams: {} });
    };
    RequestlistComponent.prototype.setPage = function (page) {
        var _this = this;
        if (page < 1) {
            return;
        }
        this.currentpage = page;
        this.userService.getRequests(this.selectedCity, this.selectedSubject, page).subscribe(function (urequestsResult) {
            console.log(urequestsResult);
            // get pager object from service
            _this.pager = _this.pagerService.getPager(urequestsResult.TotalRows, page);
            _this.requests = urequestsResult.Entities;
            _this.nrOfRequests = urequestsResult.TotalRows;
        });
    };
    RequestlistComponent.prototype.open = function (content, options) {
        var _this = this;
        this.modalService.open(content, options).result.then(function (result) {
            _this.closeResult = "Closed with: " + result;
        }, function (reason) {
            _this.closeResult = "Dismissed " + _this.getDismissReason(reason);
        });
    };
    RequestlistComponent.prototype.getDismissReason = function (reason) {
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
    RequestlistComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: 'requestlist',
            templateUrl: 'html/requestlist.component.html',
            providers: [users_service_1.UsersService, pager_service_1.PagerService]
        }),
        __metadata("design:paramtypes", [users_service_1.UsersService,
            ng_bootstrap_1.NgbModal,
            router_1.ActivatedRoute,
            pager_service_1.PagerService,
            router_1.Router])
    ], RequestlistComponent);
    return RequestlistComponent;
}());
exports.RequestlistComponent = RequestlistComponent;
//# sourceMappingURL=requestlist.component.js.map