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
var router_1 = require("@angular/router");
var ng2_img_cropper_1 = require("ng2-img-cropper");
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var ProfileComponent = /** @class */ (function () {
    function ProfileComponent(profileService, router, activateRoute, modalService) {
        this.profileService = profileService;
        this.router = router;
        this.activateRoute = activateRoute;
        this.modalService = modalService;
        this.cropperSettings = new ng2_img_cropper_1.CropperSettings();
        this.cropperSettings.width = 100;
        this.cropperSettings.height = 100;
        this.cropperSettings.croppedWidth = 100;
        this.cropperSettings.croppedHeight = 100;
        this.cropperSettings.canvasWidth = 400;
        this.cropperSettings.canvasHeight = 300;
        this.cropperSettings.rounded = true;
        this.data = {};
        this.editmodeprofile = false;
    }
    ProfileComponent.prototype.ngOnInit = function () {
    };
    ProfileComponent.prototype.editProfile = function () {
        this.editmodeprofile = !this.editmodeprofile;
    };
    ProfileComponent.prototype.open = function (content) {
        var _this = this;
        this.modalService.open(content).result.then(function (result) {
            _this.closeResult = "Closed with: " + result;
        }, function (reason) {
            _this.closeResult = "Dismissed " + _this.getDismissReason(reason);
        });
    };
    ProfileComponent.prototype.getDismissReason = function (reason) {
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
    ProfileComponent.prototype.fileChangeListener = function (event) {
        var image = new Image();
        var file = event.target.files[0];
        var myReader = new FileReader();
        var that = this;
        myReader.onloadend = function (loadEvent) {
            image.src = loadEvent.target.result;
            that.cropper.setImage(image);
        };
        myReader.readAsDataURL(file);
    };
    ProfileComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            templateUrl: 'profile.component.html',
            providers: [profile_service_1.ProfileService, ng2_img_cropper_1.ImageCropperComponent]
        }),
        __metadata("design:paramtypes", [profile_service_1.ProfileService,
            router_1.Router,
            router_1.ActivatedRoute,
            ng_bootstrap_1.NgbModal])
    ], ProfileComponent);
    return ProfileComponent;
}());
exports.ProfileComponent = ProfileComponent;
//# sourceMappingURL=profile.component.js.map