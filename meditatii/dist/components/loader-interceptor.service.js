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
var http_1 = require("@angular/common/http");
var operators_1 = require("rxjs/operators");
var loader_service_1 = require("../services/loader.service");
var LoaderInterceptorService = (function () {
    function LoaderInterceptorService(loaderService) {
        this.loaderService = loaderService;
    }
    LoaderInterceptorService.prototype.intercept = function (req, next) {
        var _this = this;
        this.showLoader();
        return next.handle(req).pipe(operators_1.tap(function (event) {
            if (event instanceof http_1.HttpResponse) {
                console.log('---> status:', event.status);
                _this.onEnd();
            }
        }, function (err) {
            _this.onEnd();
        }));
    };
    LoaderInterceptorService.prototype.onEnd = function () {
        this.hideLoader();
    };
    LoaderInterceptorService.prototype.showLoader = function () {
        this.loaderService.show();
    };
    LoaderInterceptorService.prototype.hideLoader = function () {
        this.loaderService.hide();
    };
    LoaderInterceptorService = __decorate([
        core_1.Injectable({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [loader_service_1.LoaderService])
    ], LoaderInterceptorService);
    return LoaderInterceptorService;
}());
exports.LoaderInterceptorService = LoaderInterceptorService;
