import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { TeacherlistComponent } from './components/teacherlist.component';
import { TeacherProfileComponent } from './components/teacherprofile.component';
import { BackendComponent } from './components/backend/backend.component';
import { MessagesComponent } from './components/backend/messages.component';
import { ProfileComponent } from './components/backend/profile.component';
import { AppoitmentsComponent } from './components/backend/appoitments.component';

const appRoutes: Routes = [
    {
        path: '',
        component: AppComponent
    },
    {
        path: 'teacher',
        component: TeacherlistComponent
	},
	{
        path: 'teacherprofile/:id',
        component: TeacherProfileComponent
	},
	{
		path: 'u',
		component: BackendComponent
	},
	{
		path: 'u/messages',
		component: BackendComponent,
		children: [{ path: '', outlet: 'maincontent', component:MessagesComponent }]
	},
	{
		path: 'u/profile',
		component: BackendComponent,
		children: [{ path: '', outlet: 'maincontent', component:ProfileComponent }]
	},
	{
		path: 'u/appoitments',
		component: BackendComponent,
		children: [{ path: '', outlet: 'maincontent', component: AppoitmentsComponent }]
	}
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);