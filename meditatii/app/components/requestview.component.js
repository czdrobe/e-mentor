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
var profile_service_1 = require("../services/profile.service");
var messages_service_1 = require("../services/messages.service");
var rxjs_1 = require("rxjs");
var operators_1 = require("rxjs/operators");
var pager_service_1 = require("../services/pager.service");
var RequestviewComponent = /** @class */ (function () {
    function RequestviewComponent(messagesService, userService, modalService, activateRoute, profileService, pagerService, router) {
        this.messagesService = messagesService;
        this.userService = userService;
        this.modalService = modalService;
        this.activateRoute = activateRoute;
        this.profileService = profileService;
        this.pagerService = pagerService;
        this.router = router;
        this._success = new rxjs_1.Subject();
    }
    RequestviewComponent.prototype.ngOnInit = function () {
        var _this = this;
        this._success.subscribe(function (message) { return _this.successMessage = message; });
        this._success.pipe(operators_1.debounceTime(5000)).subscribe(function () { return _this.successMessage = null; });
        this.profileService.getCurrentProfile().subscribe(function (results) {
            console.log(results);
            _this.currentUser = results;
        });
        this.activateRoute.params.subscribe(function (p) {
            if (p.hasOwnProperty("id")) {
                var id = p.id;
                _this.userService.getRequest(id).subscribe(function (requestResult) {
                    console.log(requestResult);
                    _this.request = requestResult;
                });
            }
        });
    };
    RequestviewComponent.prototype.sendMessage = function () {
        var _this = this;
        this.messagesService.saveMessageForRequest(this.request.RequestCode, this.newMessage).subscribe(function (result) {
            _this.newMessage = "";
            _this._success.next("Mesajul a fost trimis");
        });
        console.log('newmessage value:' + this.newMessage);
    };
    RequestviewComponent.prototype.checkSendMessage = function (messageContent, subscriptionContent) {
        if (this.currentUser == null) {
            this.router.navigate(['/Account/Login'], { queryParams: {} }).then(function () {
                window.location.reload();
            });
        }
        else {
            if (this.currentUser.IsSubscriptionOk) {
                //this.modalService.open(messageContent);
                this.modalService.open(messageContent);
            }
            else {
                //this.open(content1,null);
                //this.modalService.open(newmessage);
                this.modalService.open(subscriptionContent);
            }
        }
    };
    RequestviewComponent.prototype.open = function (content, options) {
        var _this = this;
        this.modalService.open(content, options).result.then(function (result) {
            _this.closeResult = "Closed with: " + result;
        }, function (reason) {
            _this.closeResult = "Dismissed " + _this.getDismissReason(reason);
        });
    };
    RequestviewComponent.prototype.getDismissReason = function (reason) {
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
    RequestviewComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: 'requestview',
            templateUrl: 'html/requestview.component.html',
            providers: [users_service_1.UsersService, pager_service_1.PagerService, profile_service_1.ProfileService, messages_service_1.MessagesService]
        }),
        __metadata("design:paramtypes", [messages_service_1.MessagesService,
            users_service_1.UsersService,
            ng_bootstrap_1.NgbModal,
            router_1.ActivatedRoute,
            profile_service_1.ProfileService,
            pager_service_1.PagerService,
            router_1.Router])
    ], RequestviewComponent);
    return RequestviewComponent;
}());
exports.RequestviewComponent = RequestviewComponent;
//# sourceMappingURL=requestview.component.js.map