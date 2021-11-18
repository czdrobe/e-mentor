import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import * as _ from 'underscore';

import { ProfileService } from '../../services/profile.service';
import { AppoitmentsService } from '../../services/appoitments.service';

@Component({
    moduleId: module.id,
    selector: 'backend',
    templateUrl: 'html/mysubscription.component.html',
    providers: [ProfileService, AppoitmentsService]
})
export class MysubscriptionComponent {
    
    currentUser:any;
    currentPayment:any;
    lstPayments:any[];
    activeProduct:string;

    constructor(
        private router: Router,
        private activateRoute: ActivatedRoute,
        private profileService: ProfileService,
        private appoitmentsService: AppoitmentsService
        )
    {
        
    }

    ngOnInit() {

        this.profileService.getCurrentProfile().subscribe((results:any) => {
			this.currentUser = results;
		});

        this.appoitmentsService.getCurrentPayment().subscribe((results:any) => {
			this.currentPayment = results;
            if (this.currentPayment != null)
            {
                if (this.currentPayment.Product == "31")
                {
                    this.activeProduct = "1 lună";
                }
                else if (this.currentPayment.Product == "93")
                {
                    this.activeProduct = "3 luni";
                }
                else if (this.currentPayment.Product == "365")
                {
                    this.activeProduct = "1 an";
                }
            }
		});
        this.appoitmentsService.getPaymentForUser().subscribe((results:any) => {
			this.lstPayments = results.Entities;
		}); 
    }

}