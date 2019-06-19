import { Component } from '@angular/core';
import { MessagesService } from '../../services/messages.service';
import { Router, ActivatedRoute } from '@angular/router';
import * as _ from 'underscore';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import {Subject} from 'rxjs';
import {debounceTime} from 'rxjs/operators';

import { PagerService } from '../../services/pager.service';

import {MessageModel} from '../../models/messageModel'

@Component({
	moduleId: module.id,
	templateUrl: 'messages.component.html',
	providers: [PagerService, MessagesService]
})
export class MessagesComponent {
	messages: Message[];
	mentors: MentorMessage[];
	pager: any = {};
	currentMentorId:number;
	currentpage: number;
	closeResult:string;
	private _success = new Subject<string>();
	successMessage: string;

	model = new MessageModel("");

	constructor(
		private messagesService: MessagesService,
        private router: Router,
		private activateRoute: ActivatedRoute,
		private pagerService: PagerService,
		private modalService: NgbModal,
    )
    {
         
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

		this.activateRoute.queryParams.subscribe(params => {
			//this.currentpage = (params.hasOwnProperty("page") ? parseInt(params.page) : 1);
			//this.setPage(this.currentpage);
			this.messagesService.getUsers().subscribe((results:any) => {
				console.log(results);
				this.mentors = results;
				console.log(this.mentors);
			});
		});

	}

	updateUrl() {
		//this.router.navigate(['/Teacher'], { queryParams: { maincategory: this.selectedMainCategory, category: this.selectedCategory, cycle: this.selectedCycle, page: this.currentpage } });
	}

	LoadMessages(mentorMessage:MentorMessage) {
			this.currentpage = 1;
			this.currentMentorId =mentorMessage.Id;
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
		this.messagesService.getMessages(this.currentMentorId, page).subscribe((messagesResult:any) => {
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
	name: string,
	nrofmessage: number,
	isread: boolean
}