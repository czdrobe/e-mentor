"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var app_component_1 = require("./app.component");
var teacherlist_component_1 = require("./components/teacherlist.component");
var teacherprofile_component_1 = require("./components/teacherprofile.component");
var backend_component_1 = require("./components/backend/backend.component");
var messages_component_1 = require("./components/backend/messages.component");
var profile_component_1 = require("./components/backend/profile.component");
var appoitments_component_1 = require("./components/backend/appoitments.component");
var appRoutes = [
    {
        path: '',
        component: app_component_1.AppComponent
    },
    {
        path: 'teacher',
        component: teacherlist_component_1.TeacherlistComponent
    },
    {
        path: 'teacherprofile/:id',
        component: teacherprofile_component_1.TeacherProfileComponent
    },
    {
        path: 'u',
        component: backend_component_1.BackendComponent
    },
    {
        path: 'u/messages',
        component: backend_component_1.BackendComponent,
        children: [{ path: '', outlet: 'maincontent', component: messages_component_1.MessagesComponent }]
    },
    {
        path: 'u/profile',
        component: backend_component_1.BackendComponent,
        children: [{ path: '', outlet: 'maincontent', component: profile_component_1.ProfileComponent }]
    },
    {
        path: 'u/appoitments',
        component: backend_component_1.BackendComponent,
        children: [{ path: '', outlet: 'maincontent', component: appoitments_component_1.AppoitmentsComponent }]
    }
];
exports.routing = router_1.RouterModule.forRoot(appRoutes);
//# sourceMappingURL=app.routing.js.map