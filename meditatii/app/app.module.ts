import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { TeacherlistComponent } from './components/teacherlist.component';
import { TeacherProfileComponent } from './components/teacherprofile.component';
import { RoomComponent } from './components/room.component';
import { BackendComponent } from './components/backend/backend.component';
import { BackEndMenuComponent } from './components/backend/menu.component';
import { MessagesComponent } from './components/backend/messages.component';
import { ProfileComponent } from './components/backend/profile.component';
import { AppoitmentsComponent } from './components/backend/appoitments.component';
import { CustomCalendarComponent } from './components/calendar.component';
import {ImageCropperComponent} from 'ng2-img-cropper';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';

import {LoaderComponent} from './components/loader.component';
import { LoaderInterceptorService } from './components/loader-interceptor.service';

import {FilterByMateriaPipe} from './components/listfilter.pip';

import { routing } from './app.routing';

import { BaseRequestOptions } from '@angular/http';
import { Http,RequestOptions } from '@angular/http';

@NgModule({
	imports: [NgbModule.forRoot(), BrowserModule, HttpClientModule, HttpModule, routing, FormsModule],
	declarations: [FilterByMateriaPipe, AppComponent, LoaderComponent, TeacherlistComponent, TeacherProfileComponent, RoomComponent, BackendComponent, BackEndMenuComponent, MessagesComponent, ProfileComponent,AppoitmentsComponent, CustomCalendarComponent, ImageCropperComponent],
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
