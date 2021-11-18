import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ReportService } from '../../services/report.service';
import { ProfileService } from '../../services/profile.service';
import * as _ from 'underscore';
import {MessageModel} from '../../models/messageModel';
import { MessagesService } from '../../services/messages.service';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';

@Component({
	moduleId: module.id,
	templateUrl: 'html/appoitmentreport.component.html',
    providers: [ReportService, ProfileService, MessagesService]
})
export class AppoitmentReportComponent {
    appoitmentsTeacherReport: AppoitmentTeacherReport[];
    currentProfile: any;
    isDebug:boolean;
    today:any;
    balance:any;

    constructor(
        private router: Router,
        private activateRoute: ActivatedRoute,
        private reportService: ReportService,
        private profileService: ProfileService,
        private messagesService: MessagesService,
        private modalService: NgbModal,
    )
    {
        
    }

    ngOnInit() {
        var currentDate = new Date();
        this.today = currentDate.toLocaleDateString();

        this.isDebug = false;

        this.profileService.getCurrentProfile().subscribe((result:any) => {
            this.currentProfile = result;

            this.loadAppoitmentsTeacherReport();
        });

        
        this.activateRoute.queryParams.subscribe(params => {
          if (params.hasOwnProperty("debug"))
          {
              this.isDebug = true;
          }
        });
    }

    loadAppoitmentsTeacherReport()
    {
        this.reportService.getAppoitmentReport(1).subscribe((results:any) => {
            this.appoitmentsTeacherReport = results.Entities;
            //console.log(this.appoitments);
        });

        this.reportService.getBalance().subscribe((results:any) => {
          this.balance = results;
        });
    }

 }

interface AppoitmentTeacherReport
{
  AppoitmentId: number,
	StartDate: Date,
  EndDate: Date,
  Price: number,
  Learner: User,
  Payment: Payment,
  PayOutTeacher: Date,
  PayOutPlatform: Date,
  PayOutTax: Date
}

interface User
{
  Id:number,
  FirstName: string,
  LastName: string,
  ProfileImageUrl: string
}

export class Payment
{
  AppoitmentId: number;
  Price:number;
  Status:number
}

