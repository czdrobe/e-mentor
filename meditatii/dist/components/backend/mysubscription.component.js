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
var router_1 = require("@angular/router");
var profile_service_1 = require("../../services/profile.service");
var appoitments_service_1 = require("../../services/appoitments.service");
var MysubscriptionComponent = /** @class */ (function () {
    function MysubscriptionComponent(router, activateRoute, profileService, appoitmentsService) {
        this.router = router;
        this.activateRoute = activateRoute;
        this.profileService = profileService;
        this.appoitmentsService = appoitmentsService;
    }
    MysubscriptionComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.profileService.getCurrentProfile().subscribe(function (results) {
            _this.currentUser = results;
        });
        this.appoitmentsService.getCurrentPayment().subscribe(function (results) {
            _this.currentPayment = results;
            if (_this.currentPayment != null) {
                if (_this.currentPayment.Product == "31") {
                    _this.activeProduct = "1 lună";
                }
                else if (_this.currentPayment.Product == "93") {
                    _this.activeProduct = "3 luni";
                }
                else if (_this.currentPayment.Product == "365") {
                    _this.activeProduct = "1 an";
                }
            }
        });
        this.appoitmentsService.getPaymentForUser().subscribe(function (results) {
            _this.lstPayments = results.Entities;
        });
    };
    MysubscriptionComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: 'backend',
            templateUrl: 'html/mysubscription.component.html',
            providers: [profile_service_1.ProfileService, appoitments_service_1.AppoitmentsService]
        }),
        __metadata("design:paramtypes", [router_1.Router,
            router_1.ActivatedRoute,
            profile_service_1.ProfileService,
            appoitments_service_1.AppoitmentsService])
    ], MysubscriptionComponent);
    return MysubscriptionComponent;
}());
exports.MysubscriptionComponent = MysubscriptionComponent;
//# sourceMappingURL=mysubscription.component.js.map