import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AppoitmentsService } from '../../services/appoitments.service';
import * as _ from 'underscore';

@Component({
	moduleId: module.id,
	templateUrl: 'appoitments.component.html',
    providers: [AppoitmentsService]
})
export class AppoitmentsComponent {
    appoitments: Appoitment[];
    currentDate: Date;
    constructor(
        private router: Router,
        private activateRoute: ActivatedRoute,
        private appoitmentsService: AppoitmentsService,
    )
    {
        
    }

    ngOnInit() {
        this.currentDate = new Date();
        this.appoitmentsService.getAppoitments(1).subscribe((results:any) => {
            console.log(results);
            this.appoitments = results.Entities;
            console.log(this.appoitments);
        });
    }
}

interface Appoitment
{
	StartDate: Date,
	EndDate: Date,
	
	Added: string,
	isRead: boolean,
	SenderName: string
}

