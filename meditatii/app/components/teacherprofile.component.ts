import { Component, NgZone, ChangeDetectorRef } from '@angular/core';
import { Router, ActivatedRoute, Data } from '@angular/router';
import { UsersService } from '../services/users.service';
import * as _ from 'underscore'; 
import { PagerService } from '../services/pager.service'; 
import { NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import { ProfileService } from '../services/profile.service';
import { MessagesService } from '../services/messages.service';

declare var $: any;
declare var jquery: any;

@Component({
    moduleId: module.id,
    selector: 'teacherprofile',
    templateUrl: 'teacherprofile.component.html',
    providers: [UsersService, ProfileService, MessagesService]
})
export class TeacherProfileComponent {

    profileid :number;
	teacher: Teacher;
    availableTime: number[];
    selectedDate:any;
    selectedDateToDisplay: string;
    selectedTime:number;
    closeResult:string;
    isCurrentUserIsLoggedIn: boolean;
    newMessage: string;
    agreeCheckBox:boolean;
    modalRef:any;

    constructor(
        private userService: UsersService,
        private router: Router,
		private activateRoute: ActivatedRoute,
        private changeDetectorRef: ChangeDetectorRef, 
        private modalService: NgbModal,
        private profileService: ProfileService,
        private messagesService: MessagesService,
    )
    {
        
    }

    ngOnInit() {
        this.agreeCheckBox = false;
        this.selectedDate = null;
        this.availableTime =  [14, 15, 18, 19];

        this.profileService.getCurrentProfile().subscribe((profile:any) => {
            this.isCurrentUserIsLoggedIn = profile != null;
        });

       this.activateRoute.params.subscribe(p => {
            if (p.hasOwnProperty("id"))
            {
                this.profileid = p.id;
                this.userService.getUser(this.profileid).subscribe((usersResult:any) => {
                    console.log(usersResult);           
                    this.teacher = usersResult;
                    
                    this.getAvailableTime(this.profileid, '');
                });
            }
            console.log(p);
       });
    }

    setTime(starttime:number)
    {
        this.selectedTime = starttime;
    }
    
    onSelectDate($event:any) {
        //alert($event);
        if (this.selectedDate != null)
        {
            this.selectedDate.selected = false;
        }
        $event.selected = !$event.selected;
        if ($event.selected)
        {
            this.selectedDate = $event;
        }
        var date = $event.mDate.year() + "-" + ($event.mDate.month() + 1)  + "-" + $event.mDate.date();
        this.getAvailableTime(this.profileid, date);
    }

    getAvailableTime(id:number, date:any)
    {
        var dateToCheck = "";
        if (date !=null && date != "")
        {
            dateToCheck = date;
        }
        else
        {
            if (this.selectedDate != null)
            {
                dateToCheck = this.selectedDate.mDate.year() + "-" + (this.selectedDate.mDate.month() + 1)  + "-" + this.selectedDate.mDate.date();
            }
            else
            {
                var today = new Date();
                var dd = today.getDate();
                var mm = today.getMonth()+1; //January is 0!
                var yyyy = today.getFullYear();
                dateToCheck = yyyy + "-" + mm + "-" + dd;    
            }
        }
         
        this.userService.getUserAvaiabilityForDay(id, dateToCheck).subscribe( avaiability => {
            this.selectedDateToDisplay = dateToCheck;
            this.availableTime = avaiability.Entities;
            console.log(this.availableTime);
        });
    }

    getCurrentDate()
    {
        if (this.selectedDate != null)
        {
            return this.selectedDate.mDate.year() + "-" + (this.selectedDate.mDate.month() + 1)  + "-" + this.selectedDate.mDate.date()
        }
        else
        {
            var today = new Date();
            return today.getFullYear() + "-" + (today.getMonth()+1) + "-" + today.getDate();
        }
    }
    saveAppoitment()
    {
        if (this.agreeCheckBox)
        {
            let localModalRef = this.modalRef;

            this.userService.saveAppoitment(this.profileid, this.getCurrentDate(),  this.selectedTime).subscribe( result => {
                console.log(result);
                this.getAvailableTime(this.profileid, '');
                this.agreeCheckBox = false;
                localModalRef.close();
            });
        }
    }
      
    testfunction()
    {
        alert('hello');
    }

    sendMessage()
    {
        this.messagesService.saveMessage(this.profileid, this.newMessage).subscribe(result => 
            {
                this.newMessage = "";	
            });
        console.log('newmessage value:' + this.newMessage);
    }

	open(content:any, options: any) {

        this.modalRef = this.modalService.open(content);
        this.modalRef.result.then((result:any) => {
          this.closeResult = `Closed with: ${result}`;
        }, (reasonnew:any) => {
            this.closeResult = `Dismissed ${this.getDismissReason(reasonnew)}`;
        });
      }

    private getDismissReason(reason: any): string {
		if (reason === ModalDismissReasons.ESC) {
		  return 'by pressing ESC';
		} else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
		  return 'by clicking on a backdrop';
		} else {
		  return  `with: ${reason}`;
		}
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

