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
var appoitments_service_1 = require("../../services/appoitments.service");
var AppoitmentsComponent = /** @class */ (function () {
    function AppoitmentsComponent(router, activateRoute, appoitmentsService) {
        this.router = router;
        this.activateRoute = activateRoute;
        this.appoitmentsService = appoitmentsService;
    }
    AppoitmentsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.currentDate = new Date();
        this.appoitmentsService.getAppoitments(1).subscribe(function (results) {
            console.log(results);
            _this.appoitments = results.Entities;
            console.log(_this.appoitments);
        });
    };
    AppoitmentsComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            templateUrl: 'appoitments.component.html',
            providers: [appoitments_service_1.AppoitmentsService]
        }),
        __metadata("design:paramtypes", [router_1.Router,
            router_1.ActivatedRoute,
            appoitments_service_1.AppoitmentsService])
    ], AppoitmentsComponent);
    return AppoitmentsComponent;
}());
exports.AppoitmentsComponent = AppoitmentsComponent;
//# sourceMappingURL=appoitments.component.js.map