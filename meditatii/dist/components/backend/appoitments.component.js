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
var profile_service_1 = require("../../services/profile.service");
var messages_service_1 = require("../../services/messages.service");
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var AppoitmentsComponent = /** @class */ (function () {
    function AppoitmentsComponent(router, activateRoute, appoitmentsService, profileService, messagesService, modalService) {
        this.router = router;
        this.activateRoute = activateRoute;
        this.appoitmentsService = appoitmentsService;
        this.profileService = profileService;
        this.messagesService = messagesService;
        this.modalService = modalService;
    }
    AppoitmentsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.selectedRate = -1;
        this.selectedAppoitment = -1;
        this.currentDate = new Date();
        this.selectedPayment = [];
        this.isDebug = false;
        this.profileService.getCurrentProfile().subscribe(function (result) {
            _this.currentProfile = result;
            _this.loadAppoitments();
            _this.loadOldAppoitments();
        });
        this.activateRoute.queryParams.subscribe(function (params) {
            if (params.hasOwnProperty("debug")) {
                _this.isDebug = true;
            }
        });
    };
    AppoitmentsComponent.prototype.deleteAppoitment = function (appoitmentid) {
        var _this = this;
        if (confirm("Esit sigur că vrei să anulezi această programare ?")) {
            this.appoitmentsService.deleteAppoitment(appoitmentid).subscribe(function (result) {
                console.log(result);
                _this.loadAppoitments();
            });
        }
    };
    AppoitmentsComponent.prototype.loadChatHistoryForAppoitment = function (appoitmentid) {
        var _this = this;
        this.appoitmentsService.getChatForAppoitment(appoitmentid).subscribe(function (results) {
            console.log(results);
            _this.appoitmentchats = results.Entities;
            console.log(_this.appoitmentchats);
        });
    };
    AppoitmentsComponent.prototype.loadAppoitments = function () {
        var _this = this;
        this.appoitmentsService.getActiveAppoitments(1).subscribe(function (results) {
            console.log(results);
            _this.appoitments = results.Entities;
            console.log(_this.appoitments);
        });
    };
    AppoitmentsComponent.prototype.loadOldAppoitments = function () {
        var _this = this;
        this.appoitmentsService.getOldAppoitments(1).subscribe(function (results) {
            console.log(results);
            _this.oldAppoitments = results.Entities;
            console.log(_this.oldAppoitments);
        });
    };
    AppoitmentsComponent.prototype.sendMessage = function () {
        var _this = this;
        this.messagesService.saveMessage(this.selectedUserId, this.newMessage).subscribe(function (result) {
            _this.newMessage = "";
        });
        console.log('newmessage value:' + this.newMessage);
    };
    AppoitmentsComponent.prototype.selectUser = function (userId) {
        this.selectedUserId = userId;
    };
    AppoitmentsComponent.prototype.acceptAppoitment = function (appoitmentId) {
        var _this = this;
        if (confirm("Esit sigur că accepți această programare ?")) {
            this.appoitmentsService.acceptByTeacher(appoitmentId).subscribe(function (results) {
                _this.loadAppoitments();
            });
        }
    };
    AppoitmentsComponent.prototype.selectPayment = function (appoitmentId, price, button) {
        if (button.tagName != "BUTTON") {
            button = button.parentElement;
        }
        var item = this.selectedPayment.find(function (x) { return x.AppoitmentId == appoitmentId; });
        if (item == null) {
            //ADD
            item = new Payment();
            item.AppoitmentId = appoitmentId;
            item.Price = price;
            this.selectedPayment.push(item);
            //updte button
            button.className = button.className.replace('btn-danger', 'btn-ligth');
            button.innerHTML = "X   Renunta";
            button.style = "color:red";
        }
        else {
            //REMOVE
            var index = this.selectedPayment.indexOf(item);
            if (index !== -1) {
                this.selectedPayment.splice(index, 1);
            }
            //updte button
            button.className = button.className.replace('btn-ligth', 'btn-danger');
            button.innerHTML = "Achita";
            button.style = "color:white";
        }
        this.calculateTotalToPay();
    };
    AppoitmentsComponent.prototype.pay = function () {
        var lstOfAppoitments = this.selectedPayment.map(function (el) { return el.AppoitmentId; });
        /*if (this.isDebug)
        {
          this.appoitmentsService.payment(lstOfAppoitments).subscribe(result =>
            {
              this.loadAppoitments();
              this.selectedPayment = [];
            });
        }
        else
        {*/
        //TODO: need to implement real payment
        window.location.href = "/payment/" + lstOfAppoitments.join(',');
        //}
    };
    AppoitmentsComponent.prototype.needToPay = function (appoitment) {
        if (!appoitment.AcceptedByTeacher) {
            //do not show payment button if teacher not accepted the appoitment 
            return false;
        }
        var isPaid = appoitment.Payment != null && appoitment.Payment.Status == 1;
        return !isPaid;
    };
    AppoitmentsComponent.prototype.calculateTotalToPay = function () {
        var total = 0;
        for (var _i = 0, _a = this.selectedPayment; _i < _a.length; _i++) {
            var entry = _a[_i];
            total += entry.Price;
        }
        this.totalToPay = total;
    };
    AppoitmentsComponent.prototype.open = function (content, options) {
        var _this = this;
        this.modalRef = this.modalService.open(content);
        this.modalRef.result.then(function (result) {
            _this.closeResult = "Closed with: " + result;
        }, function (reasonnew) {
            _this.closeResult = "Dismissed " + _this.getDismissReason(reasonnew);
        });
    };
    AppoitmentsComponent.prototype.getDismissReason = function (reason) {
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
    AppoitmentsComponent.prototype.openNewTab = function (url) {
        window.open(url, url);
    };
    AppoitmentsComponent.prototype.selectRate = function (rate) {
        this.selectedRate = rate;
    };
    AppoitmentsComponent.prototype.saveRate = function () {
        var localModalRef = this.modalRef;
        if (this.selectedRate > -1 && this.selectedAppoitment > -1) {
            /*this.appoitmentsService.saveTeacherRating(this.selectedAppoitment, this.selectedRate).subscribe(result =>
              {
                this.loadOldAppoitments();
                this.selectedRate = -1;
                this.selectedAppoitment =-1;
                localModalRef.close();
              });*/
        }
    };
    AppoitmentsComponent.prototype.selectAppoitment = function (appoitmentId) {
        this.selectedAppoitment = appoitmentId;
    };
    AppoitmentsComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            templateUrl: 'html/appoitments.component.html',
            providers: [appoitments_service_1.AppoitmentsService, profile_service_1.ProfileService, messages_service_1.MessagesService]
        }),
        __metadata("design:paramtypes", [router_1.Router,
            router_1.ActivatedRoute,
            appoitments_service_1.AppoitmentsService,
            profile_service_1.ProfileService,
            messages_service_1.MessagesService,
            ng_bootstrap_1.NgbModal])
    ], AppoitmentsComponent);
    return AppoitmentsComponent;
}());
exports.AppoitmentsComponent = AppoitmentsComponent;
var Payment = /** @class */ (function () {
    function Payment() {
    }
    return Payment;
}());
exports.Payment = Payment;
//# sourceMappingURL=appoitments.component.js.map