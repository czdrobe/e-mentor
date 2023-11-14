import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { TeacherlistComponent } from './components/teacherlist.component';
import { RequestlistComponent } from './components/requestlist.component';
import { RequestviewComponent } from './components/requestview.component';
import { RequestnewComponent } from './components/requestnew.component';
import { TeacherProfileComponent } from './components/teacherprofile.component';
import { RoomComponent } from './components/room.component';
import { BackendComponent } from './components/backend/backend.component';
import { BackEndMenuComponent } from './components/backend/menu.component';
import { MessagesComponent } from './components/backend/messages.component';
import { ProfileComponent } from './components/backend/profile.component';
import { AppoitmentsComponent } from './components/backend/appoitments.component';
import { AppoitmentReportComponent } from './components/backend/appoitmentreport.component';

import { UserHomeComponent } from './components/backend/home.component';

import { SubscriptionsComponent } from './components/backend/subscriptions.component';
import { MysubscriptionComponent } from './components/backend/mysubscription.component';

import { CustomCalendarComponent } from './components/calendar.component';
import {ImageCropperComponent} from 'ng2-img-cropper';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';

import {LoaderComponent} from './components/loader.component';
import { LoaderInterceptorService } from './components/loader-interceptor.service';

import {FilterByMateriaPipe} from './components/listfilter.pip';
import {NiceDateFormatPipe} from './components/niceDateFormat.pip';

import { routing } from './app.routing';

import { BaseRequestOptions } from '@angular/http';
import { Http,RequestOptions } from '@angular/http';

@NgModule({
	imports: [NgbModule.forRoot(), BrowserModule, HttpClientModule, HttpModule, routing, FormsModule],
	declarations: [MysubscriptionComponent, SubscriptionsComponent, NiceDateFormatPipe, FilterByMateriaPipe, AppComponent, LoaderComponent, TeacherlistComponent, RequestlistComponent, RequestviewComponent, RequestnewComponent, TeacherProfileComponent, RoomComponent, BackendComponent, BackEndMenuComponent, MessagesComponent, ProfileComponent,AppoitmentsComponent,  AppoitmentReportComponent, UserHomeComponent, CustomCalendarComponent, ImageCropperComponent],
	providers: [
		{
		  provide: HTTP_INTERCEPTORS,
		  useClass: LoaderInterceptorService,
		  multi: true
		}
	  ],
	bootstrap: [AppComponent],
	
})
export class AppModule { }
