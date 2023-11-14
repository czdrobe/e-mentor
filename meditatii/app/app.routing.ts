import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, ExtraOptions } from '@angular/router';

import { AppComponent } from './app.component';
import { TeacherlistComponent } from './components/teacherlist.component';
import { RequestlistComponent } from './components/requestlist.component';
import { RequestviewComponent } from './components/requestview.component';
import { RequestnewComponent } from './components/requestnew.component';
import { TeacherProfileComponent } from './components/teacherprofile.component';
import { BackendComponent } from './components/backend/backend.component';
import { MessagesComponent } from './components/backend/messages.component';
import { ProfileComponent } from './components/backend/profile.component';
import { AppoitmentsComponent } from './components/backend/appoitments.component';
import { AppoitmentReportComponent } from './components/backend/appoitmentreport.component';
import { RoomComponent } from './components/room.component';
import { SubscriptionsComponent } from './components/backend/subscriptions.component';
import { MysubscriptionComponent } from './components/backend/mysubscription.component';
import { UserHomeComponent } from './components/backend/home.component';

const appRoutes: Routes = [
	{
		path: '',
		component: TeacherlistComponent
	},
	{
		path: ':category',
		component: TeacherlistComponent,
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
		path: 'termeni-si-conditii',
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
		path: 'adauga-solicitare-noua',
		component: RequestnewComponent,
	},
	{
		path: 'solicitare-de-meditatii',
		component: RequestlistComponent
	},
	{
		path: 'solicitare-de-meditatii/:id',
		component: RequestviewComponent
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
		path: 'u/home',
		component: BackendComponent,
		children: [{ path: '', outlet: 'maincontent', component: UserHomeComponent }]
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
		path: 'u/appoitment-report',
		component: BackendComponent,
		children: [{ path: '', outlet: 'maincontent', component: AppoitmentReportComponent }]
	},
	{
		path: 'u/abonamente',
		component: BackendComponent,
		children: [{ path: '', outlet: 'maincontent', component: SubscriptionsComponent }]
	},
	{
		path: 'u/abonamentul-meu',
		component: BackendComponent,
		children: [{ path: '', outlet: 'maincontent', component: MysubscriptionComponent }]
	},
	{
		path: 'room/:id',
		component: RoomComponent,
		
	},
	{
		path: 'payment/:id',
		component: AppComponent,
		
	},
	{
		path: 'abonamente',
		component: AppComponent,
		
	}
];

const routerOptions: ExtraOptions = {
	useHash: true,
	// ...any other options you'd like to use
};

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);