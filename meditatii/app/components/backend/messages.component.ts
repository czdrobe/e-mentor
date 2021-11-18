import { Component } from '@angular/core';
import { MessagesService } from '../../services/messages.service';
import { Router, ActivatedRoute } from '@angular/router';
import * as _ from 'underscore';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import {Subject} from 'rxjs';
import {debounceTime} from 'rxjs/operators';
import { ProfileService } from '../../services/profile.service';

import { PagerService } from '../../services/pager.service';

import {MessageModel} from '../../models/messageModel'

@Component({
	moduleId: module.id,
	templateUrl: 'html/messages.component.html',
	providers: [PagerService, MessagesService, ProfileService]
})
export class MessagesComponent {
	messages: Message[];
	mentors: MentorMessage[];
	pager: any = {};
	currentMentorId:any;
	currentpage: number;
	closeResult:string;
	private _success = new Subject<string>();
	successMessage: string;
	currentUser:any;

	model = new MessageModel("");

	constructor(
		private messagesService: MessagesService,
        private router: Router,
		private activateRoute: ActivatedRoute,
		private pagerService: PagerService,
		private modalService: NgbModal,
		private profileService: ProfileService,
    )
    {
		this.currentMentorId = "";
	}
	

	get diagnostic() { return JSON.stringify(this.model); }

	sendMessage()
	{
		this.messagesService.saveMessage(this.currentMentorId, this.model.newMessage).subscribe(result => 
			{
				this.model.newMessage = "";
				this.currentpage = 1;
				this.setPage(this.currentpage);		
				this._success.next(`Mesajul a fost trimis`);
			});
		console.log('newmessage value:' + this.model.newMessage);
	}

	ngOnInit() {
		this._success.subscribe((message) => this.successMessage = message);
		this._success.pipe(
		  debounceTime(5000)
		).subscribe(() => this.successMessage = null);

		this.profileService.getCurrentProfile().subscribe((results:any) => {
			this.currentUser = results;
		});

		this.activateRoute.queryParams.subscribe(params => {
			//this.currentpage = (params.hasOwnProperty("page") ? parseInt(params.page) : 1);
			//this.setPage(this.currentpage);
			//this.currentMentorId = 
			this.messagesService.getUsers().subscribe((results:any) => {
				console.log(results);
				this.mentors = results;
				console.log(this.mentors);
			});

			if (params.hasOwnProperty("user"))
			{
				this.currentMentorId=params.user;
				this.currentpage = 1;
				this.setPage(this.currentpage);
			}
		});

	}

	checkSendMessage(messageContent:any, subscriptionContent:any)
	{
		if (this.currentUser == null)
		{
			this.router.navigate(['/Account/Login'], { queryParams : {}});
		}

		if ( this.currentUser.IsTeacher && !this.currentUser.IsSubscriptionOk)
		{
			//this.open(content1,null);
			//this.modalService.open(newmessage);
			this.modalService.open(subscriptionContent);
		}
		else
		{
			this.modalService.open(messageContent)
		}
	}

	updateUrl() {
		//this.router.navigate(['/Teacher'], { queryParams: { maincategory: this.selectedMainCategory, category: this.selectedCategory, cycle: this.selectedCycle, page: this.currentpage } });
	}

	LoadMessages(mentorMessage:MentorMessage) {
			this.currentpage = 1;
			this.currentMentorId =mentorMessage.Code;
			this.setPage(this.currentpage);
	}

	setCurrentPage(page: number) {
		this.currentpage = page;
		this.updateUrl();
	}

	setPage(page: number) {
		if (page < 1) {
			return;
		}
		this.currentpage = page;
		this.messagesService.getMessagesByMentorCode(this.currentMentorId, page).subscribe((messagesResult:any) => {
			console.log(messagesResult);

			// get pager object from service
			this.pager = this.pagerService.getPager(messagesResult.TotalRows, page);

			this.messages = messagesResult.Entities;
		});
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

interface Message
{
	from: string,
	subject: string,
	body: string,
	Added: string,
	isRead: boolean,
	SenderName: string
}

interface MentorMessage
{
	Id: number,
	Code: string,
	name: string,
	nrofmessage: number,
	isread: boolean
}