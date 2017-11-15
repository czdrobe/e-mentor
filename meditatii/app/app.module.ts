import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { TeacherlistComponent } from './components/teacherlist.component';
import { BackendComponent } from './components/backend/backend.component';
import { BackEndMenuComponent } from './components/backend/menu.component';
import { MessagesComponent } from './components/backend/messages.component';
import { AppoitmentsComponent } from './components/backend/appoitments.component';

import { routing } from './app.routing';

@NgModule({
	imports: [BrowserModule, HttpModule, routing],
	declarations: [AppComponent, TeacherlistComponent, BackendComponent, BackEndMenuComponent, MessagesComponent,AppoitmentsComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }
