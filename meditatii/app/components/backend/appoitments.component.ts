import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import * as _ from 'underscore';

@Component({
	moduleId: module.id,
	templateUrl: 'appoitments.component.html',
    providers: []
})
export class AppoitmentsComponent {


    constructor(
        private router: Router,
        private activateRoute: ActivatedRoute,
    )
    {
        
    }

    ngOnInit() {

    }
}

