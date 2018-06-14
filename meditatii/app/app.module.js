"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var http_1 = require("@angular/http");
var forms_1 = require("@angular/forms");
var app_component_1 = require("./app.component");
var teacherlist_component_1 = require("./components/teacherlist.component");
var teacherprofile_component_1 = require("./components/teacherprofile.component");
var backend_component_1 = require("./components/backend/backend.component");
var menu_component_1 = require("./components/backend/menu.component");
var messages_component_1 = require("./components/backend/messages.component");
var profile_component_1 = require("./components/backend/profile.component");
var appoitments_component_1 = require("./components/backend/appoitments.component");
var calendar_component_1 = require("./components/calendar.component");
var ng2_img_cropper_1 = require("ng2-img-cropper");
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var app_routing_1 = require("./app.routing");
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            imports: [platform_browser_1.BrowserModule, http_1.HttpModule, app_routing_1.routing, forms_1.FormsModule, ng_bootstrap_1.NgbModule.forRoot()],
            declarations: [app_component_1.AppComponent, teacherlist_component_1.TeacherlistComponent, teacherprofile_component_1.TeacherProfileComponent, backend_component_1.BackendComponent, menu_component_1.BackEndMenuComponent, messages_component_1.MessagesComponent, profile_component_1.ProfileComponent, appoitments_component_1.AppoitmentsComponent, calendar_component_1.CustomCalendarComponent, ng2_img_cropper_1.ImageCropperComponent],
            bootstrap: [app_component_1.AppComponent]
        })
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map