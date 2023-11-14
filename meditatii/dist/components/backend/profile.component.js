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
var profile_service_1 = require("../../services/profile.service");
var category_service_1 = require("../../services/category.service");
var cycle_service_1 = require("../../services/cycle.service");
var router_1 = require("@angular/router");
var ng2_img_cropper_1 = require("ng2-img-cropper");
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var ng_bootstrap_2 = require("@ng-bootstrap/ng-bootstrap");
var ProfileComponent = /** @class */ (function () {
    function ProfileComponent(calendar, profileService, router, activateRoute, modalService, categoryService, cycleService) {
        this.calendar = calendar;
        this.profileService = profileService;
        this.router = router;
        this.activateRoute = activateRoute;
        this.modalService = modalService;
        this.categoryService = categoryService;
        this.cycleService = cycleService;
    }
    ProfileComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            templateUrl: 'html/profile.component.html',
            providers: [cycle_service_1.CycleService, category_service_1.CategoryService, profile_service_1.ProfileService, ng2_img_cropper_1.ImageCropperComponent, { provide: ng_bootstrap_2.NgbDateAdapter, useClass: ng_bootstrap_2.NgbDateNativeAdapter }],
            selector: 'ngbd-datepicker-popup'
        }),
        __metadata("design:paramtypes", [ng_bootstrap_2.NgbCalendar,
            profile_service_1.ProfileService,
            router_1.Router,
            router_1.ActivatedRoute,
            ng_bootstrap_1.NgbModal,
            category_service_1.CategoryService,
            cycle_service_1.CycleService])
    ], ProfileComponent);
    return ProfileComponent;
}());
exports.ProfileComponent = ProfileComponent;
//# sourceMappingURL=profile.component.js.map