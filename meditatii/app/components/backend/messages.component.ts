import { Component } from '@angular/core';
import { MessagesService } from '../../services/messages.service';
import { Router, ActivatedRoute } from '@angular/router';
import * as _ from 'underscore';

import { PagerService } from '../../services/pager.service';

@Component({
	moduleId: module.id,
	templateUrl: 'messages.component.html',
	providers: [PagerService, MessagesService]
})
export class MessagesComponent {
	messages: Message[];
	pager: any = {};
	currentpage: number;
	constructor(
		private messagesService: MessagesService,
        private router: Router,
		private activateRoute: ActivatedRoute,
		private pagerService: PagerService
    )
    {
        
    }

	ngOnInit() {

		this.activateRoute.queryParams.subscribe(params => {
			this.currentpage = (params.hasOwnProperty("page") ? parseInt(params.page) : 1);
			this.setPage(this.currentpage);
		});

	}

	updateUrl() {
		//this.router.navigate(['/Teacher'], { queryParams: { maincategory: this.selectedMainCategory, category: this.selectedCategory, cycle: this.selectedCycle, page: this.currentpage } });
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
		this.messagesService.getMessages(page).subscribe(messagesResult => {
			console.log(messagesResult);

			// get pager object from service
			this.pager = this.pagerService.getPager(messagesResult.TotalRows, page);

			this.messages = messagesResult.Entities;
		});
	}
}

interface Message
{
	from: string,
	subject: string,
	date: string,
	isRead: boolean
}
