import { Component, NgZone, ChangeDetectorRef } from '@angular/core';
import { Router, ActivatedRoute, Data } from '@angular/router';
import { UsersService } from '../services/users.service';
import * as _ from 'underscore'; 
import { PagerService } from '../services/pager.service'; 
import { NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import { ProfileService } from '../services/profile.service';
import { MessagesService } from '../services/messages.service';
import { AppoitmentsService } from '../services/appoitments.service';

declare var $: any;
declare var jquery: any;

@Component({
    moduleId: module.id,
    selector: 'teacherprofile',
    templateUrl: 'html/teacherprofile.component.html',
    providers: [AppoitmentsService, UsersService, ProfileService, MessagesService]
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
    phoneNumber:any;
    selectedRate:number;
    ratingText:string;
    lstRatings: TeacherRating[];
    lstRecomandations: Teacher[];
    nrOfReviews:number;
    canAddReview: boolean;
    currentUserProfile: any;

    constructor(
        private userService: UsersService,
        private router: Router,
		private activateRoute: ActivatedRoute,
        private changeDetectorRef: ChangeDetectorRef, 
        private modalService: NgbModal,
        private profileService: ProfileService,
        private messagesService: MessagesService,
        private appoitmentsService: AppoitmentsService,
    )
    {
        
    }

    ngOnInit() {
        this.canAddReview = true;
        this.nrOfReviews = 0;
        this.selectedRate = -1;
        this.ratingText = "";
        this.phoneNumber = "Telefon";
        this.agreeCheckBox = false;
        this.selectedDate = null;
        this.availableTime =  [14, 15, 18, 19];

        this.profileService.getCurrentProfile().subscribe((profile:any) => {
            this.isCurrentUserIsLoggedIn = profile != null;
            this.currentUserProfile = profile;
        });

       this.activateRoute.params.subscribe(p => {
            if (p.hasOwnProperty("id"))
            {
                this.profileid = p.id;
                this.userService.getUserByCode(this.profileid).subscribe((usersResult:any) => {
                    console.log(usersResult);           
                    this.teacher = usersResult;
                    this.getRatings(this.profileid);
                    //this.getAvailableTime(this.profileid, '');
                    this.getRecomandations();
                });
            }
            console.log(p);
       });
    }

    getPhoneNumber()
    {
        this.userService.getUserPhoneNumber(this.profileid).subscribe((phoneResult:any) => {
            this.phoneNumber = phoneResult;
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

    getAvailableTime(id:any, date:any)
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
            if (avaiability != null)
            {
                this.availableTime = avaiability.Entities;
            }
            console.log(this.availableTime);
        });
    }

    getRatings(id:any)
    {
        this.userService.getRatingsForTeacher(id).subscribe( ratings => {
            this.lstRatings = ratings;
            this.nrOfReviews = this.lstRatings.length;
            this.canAddReview = this.lstRatings.find(x => x.Student.Email == this.currentUserProfile.Email) == null;
        });
    }

    getRecomandations()
    {
        var cityid = 0;
        var categoryid = 0;

        if (this.teacher.Categories.length > 0)
        {
            categoryid = this.teacher.Categories[0].Id;
        }

        if (this.teacher.Cities.length > 0)
        {
            cityid = this.teacher.Cities[0].Id;
        }

        if (cityid > 0 && categoryid > 0)
        {
            this.userService.getRecomandations(categoryid, cityid).subscribe( usersResult => {
                this.lstRecomandations = usersResult.Entities;
            });
        }
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
    
    selectRate(rate:number)
    {
      this.selectedRate = rate;
    }

    saveRate()
    {
      if (this.selectedRate > -1)
      {
        let localModalRef = this.modalRef;

        this.appoitmentsService.saveTeacherRating(this.profileid, this.selectedRate, this.ratingText).subscribe(result =>
          {
            localModalRef.close();
            this.selectedRate = -1;
            this.ratingText = ""
            this.getRatings(this.profileid);
          });
      }
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
    Categories: Category[],
    Cities: City[]
}

interface Category {
    Id:number,
    Name:string
}

interface City {
    Id:number,
    Name:string
}

interface TeacherRating
{
    Teacher: any,
    Student: any,
    Rating: any,
    RatingText: string,
    Added:Date
}
