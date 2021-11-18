import { Component } from '@angular/core';
import { UsersService } from '../services/users.service';
import { Router, ActivatedRoute } from '@angular/router';
import * as _ from 'underscore';
import {NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { Category } from '../types/categories.type';
import { City } from '../types/city.type';
import { ProfileService } from '../services/profile.service';
import { MessagesService } from '../services/messages.service';
import {Subject} from 'rxjs';
import {debounceTime} from 'rxjs/operators';

import { PagerService } from '../services/pager.service';

@Component({
    moduleId: module.id,
    selector: 'requestview',
    templateUrl: 'html/requestview.component.html',
    providers: [UsersService, PagerService, ProfileService, MessagesService]
})

export class RequestviewComponent {
    closeResult:string;    
    request: Request;
    currentUser: any;
    newMessage: string;
    private _success = new Subject<string>();
    successMessage:string;

    constructor(
        private messagesService: MessagesService,
        private userService: UsersService,
        private modalService: NgbModal,
        private activateRoute: ActivatedRoute,
        private profileService: ProfileService,
        private pagerService: PagerService,
        private router: Router,
    )
    {

    }

    ngOnInit() {

      this._success.subscribe((message) => this.successMessage = message);
      this._success.pipe(
        debounceTime(5000)
      ).subscribe(() => this.successMessage = null);
  

      this.profileService.getCurrentProfile().subscribe((results:any) => {
        console.log(results);
        this.currentUser = results;
      });

      this.activateRoute.params.subscribe(p => {
        if (p.hasOwnProperty("id"))
        {
          var id = p.id;

          this.userService.getRequest(id).subscribe((requestResult:any) => {
            console.log(requestResult);
            this.request = requestResult;
          });
        }
      });  
    }

    sendMessage()
    {
      this.messagesService.saveMessageForRequest(this.request.RequestCode, this.newMessage).subscribe(result => 
        {
          this.newMessage = "";
          this._success.next(`Mesajul a fost trimis`);
        });
      console.log('newmessage value:' + this.newMessage);
    }    

    checkSendMessage(messageContent:any, subscriptionContent:any)
    {
      if (this.currentUser == null)
      {
        this.router.navigate(['/Account/Login'], { queryParams : {}}).then(() => {
          window.location.reload();
        });
      }
      else
      {
        if (this.currentUser.IsSubscriptionOk)
        {
          //this.modalService.open(messageContent);
          this.modalService.open(messageContent);
        }
        else
        {
          //this.open(content1,null);
          //this.modalService.open(newmessage);
          this.modalService.open(subscriptionContent);
        }
  
      }
    }
    
    open(content:any, options: any) {
      this.modalService.open(content, options).result.then((result) => {
        this.closeResult = `Closed with: ${result}`;
      }, (reason) => {
        this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
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

interface Request {
    Id: number,
    IsOnline:boolean,
    Description: string,
    LearnerId:number,
    StartDate:Date,
    EndDate:Date,
    Active:Boolean,
    Category: Category,
    City:City,
    RequestCode:string
}