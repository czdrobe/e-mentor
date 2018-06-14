import { Component, NgZone, ChangeDetectorRef } from '@angular/core';
import { Router, ActivatedRoute, Data } from '@angular/router';
import { UsersService } from '../services/users.service';
import * as _ from 'underscore'; 
import { PagerService } from '../services/pager.service'; 


declare var $: any;
declare var jquery: any;

@Component({
    moduleId: module.id,
    selector: 'teacherprofile',
    templateUrl: 'teacherprofile.component.html',
    providers: [UsersService]
})
export class TeacherProfileComponent {

    profileid :number;
	teacher: Teacher;
	availableTime: number[];
    constructor(
        private userService: UsersService,
        private router: Router,
		private activateRoute: ActivatedRoute,
		private changeDetectorRef: ChangeDetectorRef, 
    )
    {
        
    }

    ngOnInit() {
       this.availableTime =  [14, 15, 18, 19];

       this.activateRoute.params.subscribe(p => {
            if (p.hasOwnProperty("id"))
            {
                this.profileid = p.id;
                this.userService.getUser(this.profileid).subscribe((usersResult:any) => {
                    console.log(usersResult);
                
                    this.teacher = usersResult;
                    
					$(document).ready(function () {
                        
						var calendar = $('#calendar').fullCalendar(
							{
								dayClick: function (date: any, jsEvent: any, view: any) {
									console.log('clicked on ' + date.format());
                                    //this.availableTime = [20, 21, 22, 23];
                                    //that.changeDetectorRef.markForCheck();
                                    
                                    this.availableTime = [20, 21, 22, 23];
                                    console.log(this.availableTime);
								}
							}
						);

					});
                    
                });

            }
            console.log(p);
       });
    }
    
    onSelectDate($event:any) {
        alert($event);
        this.availableTime = [20, 21, 22, 23];
        console.log(this.availableTime);
      }
      
    testfunction()
    {
        alert('hello');
    }


}

interface Teacher {
    firstName: string,
    lastName: string,
    email: string,
    description: string,
    Categories: Category[]
}

interface Category {
    Name:string
}
