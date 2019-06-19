import { Component } from '@angular/core';

@Component({
    selector: 'meditatii-app',
    template: `<app-loader></app-loader>
        <router-outlet></router-outlet>
        `,
})
export class AppComponent  { }
