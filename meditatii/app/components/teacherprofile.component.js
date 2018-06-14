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
        this.availableTime = [14, 15, 18, 19];
        this.activateRoute.params.subscribe(function (p) {
            if (p.hasOwnProperty("id")) {
                _this.profileid = p.id;
                _this.userService.getUser(_this.profileid).subscribe(function (usersResult) {
                    console.log(usersResult);
                    _this.teacher = usersResult;
                    $(document).ready(function () {
                        var calendar = $('#calendar').fullCalendar({
                            dayClick: function (date, jsEvent, view) {
                                console.log('clicked on ' + date.format());
                                //this.availableTime = [20, 21, 22, 23];
                                //that.changeDetectorRef.markForCheck();
                                this.availableTime = [20, 21, 22, 23];
                                console.log(this.availableTime);
                            }
                        });
                    });
                });
            }
            console.log(p);
        });
    };
    TeacherProfileComponent.prototype.onSelectDate = function ($event) {
        alert($event);
        this.availableTime = [20, 21, 22, 23];
        console.log(this.availableTime);
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
//# sourceMappingURL=teacherprofile.component.js.map