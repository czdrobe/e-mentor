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
var report_service_1 = require("../../services/report.service");
var profile_service_1 = require("../../services/profile.service");
var messages_service_1 = require("../../services/messages.service");
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var AppoitmentReportComponent = /** @class */ (function () {
    function AppoitmentReportComponent(router, activateRoute, reportService, profileService, messagesService, modalService) {
        this.router = router;
        this.activateRoute = activateRoute;
        this.reportService = reportService;
        this.profileService = profileService;
        this.messagesService = messagesService;
        this.modalService = modalService;
    }
    AppoitmentReportComponent.prototype.ngOnInit = function () {
        var _this = this;
        var currentDate = new Date();
        this.today = currentDate.toLocaleDateString();
        this.isDebug = false;
        this.profileService.getCurrentProfile().subscribe(function (result) {
            _this.currentProfile = result;
            _this.loadAppoitmentsTeacherReport();
        });
        this.activateRoute.queryParams.subscribe(function (params) {
            if (params.hasOwnProperty("debug")) {
                _this.isDebug = true;
            }
        });
    };
    AppoitmentReportComponent.prototype.loadAppoitmentsTeacherReport = function () {
        var _this = this;
        this.reportService.getAppoitmentReport(1).subscribe(function (results) {
            _this.appoitmentsTeacherReport = results.Entities;
            //console.log(this.appoitments);
        });
        this.reportService.getBalance().subscribe(function (results) {
            _this.balance = results;
        });
    };
    AppoitmentReportComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            templateUrl: 'html/appoitmentreport.component.html',
            providers: [report_service_1.ReportService, profile_service_1.ProfileService, messages_service_1.MessagesService]
        }),
        __metadata("design:paramtypes", [router_1.Router,
            router_1.ActivatedRoute,
            report_service_1.ReportService,
            profile_service_1.ProfileService,
            messages_service_1.MessagesService,
            ng_bootstrap_1.NgbModal])
    ], AppoitmentReportComponent);
    return AppoitmentReportComponent;
}());
exports.AppoitmentReportComponent = AppoitmentReportComponent;
var Payment = /** @class */ (function () {
    function Payment() {
    }
    return Payment;
}());
exports.Payment = Payment;
//# sourceMappingURL=appoitmentreport.component.js.map