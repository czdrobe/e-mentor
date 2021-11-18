import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import * as _ from 'underscore';

@Component({
	moduleId: module.id,
	selector: 'backend-menu',
	templateUrl: 'html/menu.component.html',
    providers: []
})
export class BackEndMenuComponent {


    constructor(
        private router: Router,
        private activateRoute: ActivatedRoute,
    )
    {
        
    }

    ngOnInit() {

    }
}

