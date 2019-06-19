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
var RoomComponent = /** @class */ (function () {
    function RoomComponent(userService, router, activateRoute) {
        this.userService = userService;
        this.router = router;
        this.activateRoute = activateRoute;
    }
    RoomComponent.prototype.ngOnInit = function () {
        console.log("room component");
    };
    RoomComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: 'room',
            templateUrl: 'room.component.html',
            providers: [users_service_1.UsersService]
        }),
        __metadata("design:paramtypes", [users_service_1.UsersService,
            router_1.Router,
            router_1.ActivatedRoute])
    ], RoomComponent);
    return RoomComponent;
}());
exports.RoomComponent = RoomComponent;
//# sourceMappingURL=room.component.js.map