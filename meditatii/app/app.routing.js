"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var app_component_1 = require("./app.component");
var teacherlist_component_1 = require("./components/teacherlist.component");
var appRoutes = [
    {
        path: '',
        component: app_component_1.AppComponent
    },
    {
        path: 'Teacher',
        component: teacherlist_component_1.TeacherlistComponent
    }
];
exports.routing = router_1.RouterModule.forRoot(appRoutes);
//# sourceMappingURL=app.routing.js.map