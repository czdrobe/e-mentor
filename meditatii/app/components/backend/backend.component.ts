import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import * as _ from 'underscore';

@Component({
    moduleId: module.id,
    selector: 'backend',
    templateUrl: 'html/backend.component.html',
    providers: []
})
export class BackendComponent {
    constructor(
        private router: Router,
        private activateRoute: ActivatedRoute
    )
    {
        
    }

    ngOnInit() {
    }

}