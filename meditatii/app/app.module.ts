import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { TeacherlistComponent } from './components/teacherlist..component';
import { routing } from './app.routing';

@NgModule({
    imports: [BrowserModule, FormsModule, HttpModule, routing ],
    declarations: [AppComponent, TeacherlistComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }
