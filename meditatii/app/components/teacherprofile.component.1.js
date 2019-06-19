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
var users_service_1 = require("../services/users.service");
var TeacherProfileComponent = /** @class */ (function () {
    function TeacherProfileComponent(userService, router, activateRoute, changeDetectorRef) {
        this.userService = userService;
        this.router = router;
        this.activateRoute = activateRoute;
        this.changeDetectorRef = changeDetectorRef;
    }
    TeacherProfileComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.selectedDate = null;
        this.availableTime = [14, 15, 18, 19];
        this.activateRoute.params.subscribe(function (p) {
            if (p.hasOwnProperty("id")) {
                _this.profileid = p.id;
                _this.userService.getUser(_this.profileid).subscribe(function (usersResult) {
                    console.log(usersResult);
                    _this.teacher = usersResult;
                    _this.getAvailableTime(_this.profileid, '');
                });
            }
            console.log(p);
        });
    };
    TeacherProfileComponent.prototype.onSelectDate = function ($event) {
        //alert($event);
        if (this.selectedDate != null) {
            this.selectedDate.selected = false;
        }
        $event.selected = !$event.selected;
        if ($event.selected) {
            this.selectedDate = $event;
        }
        var date = $event.mDate.year() + "-" + ($event.mDate.month() + 1) + "-" + $event.mDate.date();
        this.getAvailableTime(this.profileid, date);
    };
    TeacherProfileComponent.prototype.getAvailableTime = function (id, date) {
        var _this = this;
        var dateToCheck;
        if (date != null && date != "") {
            dateToCheck = date;
        }
        else {
            if (this.selectedDate != null) {
                dateToCheck = this.selectedDate.mDate.year() + "-" + (this.selectedDate.mDate.month() + 1) + "-" + this.selectedDate.mDate.date();
            }
            else {
                var today = new Date();
                var dd = today.getDate();
                var mm = today.getMonth() + 1; //January is 0!
                var yyyy = today.getFullYear();
                dateToCheck = yyyy + "-" + mm + "-" + dd;
            }
        }
        this.userService.getUserAvaiabilityForDay(id, dateToCheck).subscribe(function (avaiability) {
            _this.selectedDateToDisplay = date;
            _this.availableTime = avaiability.Entities;
            console.log(_this.availableTime);
        });
    };
    TeacherProfileComponent.prototype.getCurrentDate = function () {
        if (this.selectedDate != null) {
            return this.selectedDate.mDate.year() + "-" + (this.selectedDate.mDate.month() + 1) + "-" + this.selectedDate.mDate.date();
        }
        else {
            var today = new Date();
            return today.getFullYear() + "-" + (today.getMonth() + 1) + today.getDate();
        }
    };
    TeacherProfileComponent.prototype.saveAppoitment = function (starttime) {
        var _this = this;
        this.userService.saveAppoitment(this.profileid, this.getCurrentDate(), starttime).subscribe(function (result) {
            console.log(result);
            _this.getAvailableTime(_this.profileid, '');
        });
    };
    TeacherProfileComponent.prototype.testfunction = function () {
        alert('hello');
    };
    TeacherProfileComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: 'teacherprofile',
            templateUrl: 'teacherprofile.component.html',
            providers: [users_service_1.UsersService]
        }),
        __metadata("design:paramtypes", [users_service_1.UsersService,
            router_1.Router,
            router_1.ActivatedRoute,
            core_1.ChangeDetectorRef])
    ], TeacherProfileComponent);
    return TeacherProfileComponent;
}());
exports.TeacherProfileComponent = TeacherProfileComponent;
//# sourceMappingURL=teacherprofile.component.1.js.map