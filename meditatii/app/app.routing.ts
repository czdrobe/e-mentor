import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, ExtraOptions } from '@angular/router';

import { AppComponent } from './app.component';
import { TeacherlistComponent } from './components/teacherlist.component';
import { TeacherProfileComponent } from './components/teacherprofile.component';
import { BackendComponent } from './components/backend/backend.component';
import { MessagesComponent } from './components/backend/messages.component';
import { ProfileComponent } from './components/backend/profile.component';
import { AppoitmentsComponent } from './components/backend/appoitments.component';
import { RoomComponent } from './components/room.component';

const appRoutes: Routes = [
	{
		path: '',
		component: AppComponent
	},
	{
		path: 'Contact',
		component: AppComponent
	},
	{
		path: 'CumFunctioneaza',
		component: AppComponent
	},
	{
		path: 'Account/Login',
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
		children: [{ path: '', outlet: 'maincontent', component: MessagesComponent }]
	},
	{
		path: 'u/profile',
		component: BackendComponent,
		children: [{ path: '', outlet: 'maincontent', component: ProfileComponent }]
	},
	{
		path: 'u/appoitments',
		component: BackendComponent,
		children: [{ path: '', outlet: 'maincontent', component: AppoitmentsComponent }]
	},
	{
		path: 'room/:id',
		component: RoomComponent,
		
	}
];

const routerOptions: ExtraOptions = {
	useHash: true,
	// ...any other options you'd like to use
};

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);