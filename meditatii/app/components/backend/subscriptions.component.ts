import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import * as _ from 'underscore';

@Component({
    moduleId: module.id,
    selector: 'backend',
    templateUrl: 'html/subscriptions.component.html',
    providers: []
})
export class SubscriptionsComponent {
    constructor(
        private router: Router,
        private activateRoute: ActivatedRoute
    )
    {
        
    }

    ngOnInit() {
    }

}