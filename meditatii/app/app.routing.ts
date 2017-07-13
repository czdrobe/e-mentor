import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { TeacherlistComponent } from './components/teacherlist..component';

const appRoutes: Routes = [
    {
        path: '',
        component: AppComponent
    },
    {
        path: 'Teacher',
        component: TeacherlistComponent
    }
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);