"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var app_component_1 = require("./app.component");
var teacherlist_component_1 = require("./components/teacherlist.component");
var requestlist_component_1 = require("./components/requestlist.component");
var requestview_component_1 = require("./components/requestview.component");
var requestnew_component_1 = require("./components/requestnew.component");
var teacherprofile_component_1 = require("./components/teacherprofile.component");
var backend_component_1 = require("./components/backend/backend.component");
var messages_component_1 = require("./components/backend/messages.component");
var profile_component_1 = require("./components/backend/profile.component");
var appoitments_component_1 = require("./components/backend/appoitments.component");
var appoitmentreport_component_1 = require("./components/backend/appoitmentreport.component");
var room_component_1 = require("./components/room.component");
var subscriptions_component_1 = require("./components/backend/subscriptions.component");
var mysubscription_component_1 = require("./components/backend/mysubscription.component");
var home_component_1 = require("./components/backend/home.component");
var appRoutes = [
    {
        path: '',
        component: teacherlist_component_1.TeacherlistComponent
    },
    {
        path: ':category',
        component: teacherlist_component_1.TeacherlistComponent,
    },
    {
        path: 'Contact',
        component: app_component_1.AppComponent
    },
    {
        path: 'CumFunctioneaza',
        component: app_component_1.AppComponent
    },
    {
        path: 'termeni-si-conditii',
        component: app_component_1.AppComponent
    },
    {
        path: 'Account/Login',
        component: app_component_1.AppComponent
    },
    {
        path: 'teacher',
        component: teacherlist_component_1.TeacherlistComponent
    },
    {
        path: 'adauga-solicitare-noua',
        component: requestnew_component_1.RequestnewComponent,
    },
    {
        path: 'solicitare-de-meditatii',
        component: requestlist_component_1.RequestlistComponent
    },
    {
        path: 'solicitare-de-meditatii/:id',
        component: requestview_component_1.RequestviewComponent
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
        path: 'u/home',
        component: backend_component_1.BackendComponent,
        children: [{ path: '', outlet: 'maincontent', component: home_component_1.UserHomeComponent }]
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
    },
    {
        path: 'u/appoitment-report',
        component: backend_component_1.BackendComponent,
        children: [{ path: '', outlet: 'maincontent', component: appoitmentreport_component_1.AppoitmentReportComponent }]
    },
    {
        path: 'u/abonamente',
        component: backend_component_1.BackendComponent,
        children: [{ path: '', outlet: 'maincontent', component: subscriptions_component_1.SubscriptionsComponent }]
    },
    {
        path: 'u/abonamentul-meu',
        component: backend_component_1.BackendComponent,
        children: [{ path: '', outlet: 'maincontent', component: mysubscription_component_1.MysubscriptionComponent }]
    },
    {
        path: 'room/:id',
        component: room_component_1.RoomComponent,
    },
    {
        path: 'payment/:id',
        component: app_component_1.AppComponent,
    },
    {
        path: 'abonamente',
        component: app_component_1.AppComponent,
    }
];
var routerOptions = {
    useHash: true,
};
exports.routing = router_1.RouterModule.forRoot(appRoutes);
//# sourceMappingURL=app.routing.js.map