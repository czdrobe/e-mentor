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
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var profile_service_1 = require("../services/profile.service");
var messages_service_1 = require("../services/messages.service");
var appoitments_service_1 = require("../services/appoitments.service");
var TeacherProfileComponent = /** @class */ (function () {
    function TeacherProfileComponent(userService, router, activateRoute, changeDetectorRef, modalService, profileService, messagesService, appoitmentsService) {
        this.userService = userService;
        this.router = router;
        this.activateRoute = activateRoute;
        this.changeDetectorRef = changeDetectorRef;
        this.modalService = modalService;
        this.profileService = profileService;
        this.messagesService = messagesService;
        this.appoitmentsService = appoitmentsService;
    }
    TeacherProfileComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.canAddReview = true;
        this.nrOfReviews = 0;
        this.selectedRate = -1;
        this.ratingText = "";
        this.phoneNumber = "Telefon";
        this.agreeCheckBox = false;
        this.selectedDate = null;
        this.availableTime = [14, 15, 18, 19];
        this.profileService.getCurrentProfile().subscribe(function (profile) {
            _this.isCurrentUserIsLoggedIn = profile != null;
            _this.currentUserProfile = profile;
        });
        this.activateRoute.params.subscribe(function (p) {
            if (p.hasOwnProperty("id")) {
                _this.profileid = p.id;
                _this.userService.getUserByCode(_this.profileid).subscribe(function (usersResult) {
                    console.log(usersResult);
                    _this.teacher = usersResult;
                    _this.getRatings(_this.profileid);
                    //this.getAvailableTime(this.profileid, '');
                    _this.getRecomandations();
                });
            }
            console.log(p);
        });
    };
    TeacherProfileComponent.prototype.getPhoneNumber = function () {
        var _this = this;
        this.userService.getUserPhoneNumber(this.profileid).subscribe(function (phoneResult) {
            _this.phoneNumber = phoneResult;
        });
    };
    TeacherProfileComponent.prototype.setTime = function (starttime) {
        this.selectedTime = starttime;
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
        var dateToCheck = "";
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
            _this.selectedDateToDisplay = dateToCheck;
            if (avaiability != null) {
                _this.availableTime = avaiability.Entities;
            }
            console.log(_this.availableTime);
        });
    };
    TeacherProfileComponent.prototype.getRatings = function (id) {
        var _this = this;
        this.userService.getRatingsForTeacher(id).subscribe(function (ratings) {
            _this.lstRatings = ratings;
            _this.nrOfReviews = _this.lstRatings.length;
            _this.canAddReview = _this.lstRatings.find(function (x) { return x.Student.Email == _this.currentUserProfile.Email; }) == null;
        });
    };
    TeacherProfileComponent.prototype.getRecomandations = function () {
        var _this = this;
        var cityid = 0;
        var categoryid = 0;
        if (this.teacher.Categories.length > 0) {
            categoryid = this.teacher.Categories[0].Id;
        }
        if (this.teacher.Cities.length > 0) {
            cityid = this.teacher.Cities[0].Id;
        }
        if (cityid > 0 && categoryid > 0) {
            this.userService.getRecomandations(this.profileid, categoryid, cityid).subscribe(function (usersResult) {
                _this.lstRecomandations = usersResult.Entities;
            });
        }
    };
    TeacherProfileComponent.prototype.getCurrentDate = function () {
        if (this.selectedDate != null) {
            return this.selectedDate.mDate.year() + "-" + (this.selectedDate.mDate.month() + 1) + "-" + this.selectedDate.mDate.date();
        }
        else {
            var today = new Date();
            return today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + today.getDate();
        }
    };
    TeacherProfileComponent.prototype.saveAppoitment = function () {
        var _this = this;
        if (this.agreeCheckBox) {
            var localModalRef_1 = this.modalRef;
            this.userService.saveAppoitment(this.profileid, this.getCurrentDate(), this.selectedTime).subscribe(function (result) {
                console.log(result);
                _this.getAvailableTime(_this.profileid, '');
                _this.agreeCheckBox = false;
                localModalRef_1.close();
            });
        }
    };
    TeacherProfileComponent.prototype.testfunction = function () {
        alert('hello');
    };
    TeacherProfileComponent.prototype.sendMessage = function () {
        var _this = this;
        this.messagesService.saveMessage(this.profileid, this.newMessage).subscribe(function (result) {
            _this.newMessage = "";
        });
        console.log('newmessage value:' + this.newMessage);
    };
    TeacherProfileComponent.prototype.selectRate = function (rate) {
        this.selectedRate = rate;
    };
    TeacherProfileComponent.prototype.saveRate = function () {
        var _this = this;
        if (this.selectedRate > -1) {
            var localModalRef_2 = this.modalRef;
            this.appoitmentsService.saveTeacherRating(this.profileid, this.selectedRate, this.ratingText).subscribe(function (result) {
                localModalRef_2.close();
                _this.selectedRate = -1;
                _this.ratingText = "";
                _this.getRatings(_this.profileid);
            });
        }
    };
    TeacherProfileComponent.prototype.open = function (content, options) {
        var _this = this;
        this.modalRef = this.modalService.open(content);
        this.modalRef.result.then(function (result) {
            _this.closeResult = "Closed with: " + result;
        }, function (reasonnew) {
            _this.closeResult = "Dismissed " + _this.getDismissReason(reasonnew);
        });
    };
    TeacherProfileComponent.prototype.getDismissReason = function (reason) {
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
    TeacherProfileComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: 'teacherprofile',
            templateUrl: 'html/teacherprofile.component.html',
            providers: [appoitments_service_1.AppoitmentsService, users_service_1.UsersService, profile_service_1.ProfileService, messages_service_1.MessagesService]
        }),
        __metadata("design:paramtypes", [users_service_1.UsersService,
            router_1.Router,
            router_1.ActivatedRoute,
            core_1.ChangeDetectorRef,
            ng_bootstrap_1.NgbModal,
            profile_service_1.ProfileService,
            messages_service_1.MessagesService,
            appoitments_service_1.AppoitmentsService])
    ], TeacherProfileComponent);
    return TeacherProfileComponent;
}());
exports.TeacherProfileComponent = TeacherProfileComponent;
//# sourceMappingURL=teacherprofile.component.js.map