import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';

import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { TeacherlistComponent } from './components/teacherlist.component';
import { TeacherProfileComponent } from './components/teacherprofile.component';
import { BackendComponent } from './components/backend/backend.component';
import { BackEndMenuComponent } from './components/backend/menu.component';
import { MessagesComponent } from './components/backend/messages.component';
import { ProfileComponent } from './components/backend/profile.component';
import { AppoitmentsComponent } from './components/backend/appoitments.component';
import { CustomCalendarComponent } from './components/calendar.component';
import {ImageCropperComponent} from 'ng2-img-cropper';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';

import { routing } from './app.routing';

@NgModule({
	imports: [BrowserModule, HttpModule, routing, FormsModule, NgbModule.forRoot()],
	declarations: [AppComponent, TeacherlistComponent, TeacherProfileComponent, BackendComponent, BackEndMenuComponent, MessagesComponent, ProfileComponent,AppoitmentsComponent, CustomCalendarComponent, ImageCropperComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }
