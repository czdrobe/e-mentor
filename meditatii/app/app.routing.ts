import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { TeacherlistComponent } from './components/teacherlist.component';
import { BackendComponent } from './components/backend/backend.component';
import { MessagesComponent } from './components/backend/messages.component';
import { AppoitmentsComponent } from './components/backend/appoitments.component';

const appRoutes: Routes = [
    {
        path: '',
        component: AppComponent
    },
    {
        path: 'Teacher',
        component: TeacherlistComponent
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
		path: 'u/appoitments',
		component: BackendComponent,
		children: [{ path: '', outlet: 'maincontent', component: AppoitmentsComponent }]
	}
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);