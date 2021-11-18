
import { Component, NgZone, ChangeDetectorRef } from '@angular/core';
import { Router, ActivatedRoute, Data } from '@angular/router';
import { UsersService } from '../services/users.service';
import * as _ from 'underscore'; 


declare var $: any;
declare var jquery: any;

@Component({
    moduleId: module.id,
    selector: 'room',
    templateUrl: 'html/room.component.html',
    providers: [UsersService]
})
export class RoomComponent {
    constructor(
        private userService: UsersService,
        private router: Router,
		private activateRoute: ActivatedRoute,
    )
    {
        
    }

    ngOnInit() {
        console.log("room component");
    }
    
}