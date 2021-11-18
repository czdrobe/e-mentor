import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AppoitmentsService } from '../../services/appoitments.service';
import { ProfileService } from '../../services/profile.service';
import * as _ from 'underscore';
import {MessageModel} from '../../models/messageModel';
import { MessagesService } from '../../services/messages.service';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';

@Component({
	moduleId: module.id,
	templateUrl: 'html/appoitments.component.html',
    providers: [AppoitmentsService, ProfileService, MessagesService]
})
export class AppoitmentsComponent {
    appoitments: Appoitment[];
    oldAppoitments: Appoitment[];
    currentDate: Date;
    currentProfile: any;
    newMessage: string;
    selectedUserId:number;
    closeResult:string;
    appoitmentchats:AppoitmentChat[];
    selectedRate:number;
    selectedAppoitment:number;
    modalRef:any;
    selectedPayment:Payment[];
    totalToPay:number;
    isDebug:boolean;

    constructor(
        private router: Router,
        private activateRoute: ActivatedRoute,
        private appoitmentsService: AppoitmentsService,
        private profileService: ProfileService,
        private messagesService: MessagesService,
        private modalService: NgbModal,
    )
    {
        
    }

    ngOnInit() {
        this.selectedRate = -1;
        this.selectedAppoitment = -1;
        this.currentDate = new Date();
        this.selectedPayment = [];
        this.isDebug = false;

        this.profileService.getCurrentProfile().subscribe((result:any) => {
            this.currentProfile = result;

            this.loadAppoitments();
            this.loadOldAppoitments();
        });

        
        this.activateRoute.queryParams.subscribe(params => {
          if (params.hasOwnProperty("debug"))
          {
              this.isDebug = true;
          }
        });
    }

    deleteAppoitment(appoitmentid:number)
    {
        if(confirm("Esit sigur că vrei să anulezi această programare ?")) {
            this.appoitmentsService.deleteAppoitment(appoitmentid).subscribe( result => {
                console.log(result);
                this.loadAppoitments();
            })
        }
    }

    loadChatHistoryForAppoitment(appoitmentid:number)
    {
      this.appoitmentsService.getChatForAppoitment(appoitmentid).subscribe((results:any) => {
        console.log(results);
        this.appoitmentchats = results.Entities;
        console.log(this.appoitmentchats);
    });

    }

    loadAppoitments()
    {
        this.appoitmentsService.getActiveAppoitments(1).subscribe((results:any) => {
            console.log(results);
            this.appoitments = results.Entities;
            console.log(this.appoitments);
        });
    }

    loadOldAppoitments()
    {
        this.appoitmentsService.getOldAppoitments(1).subscribe((results:any) => {
            console.log(results);
            this.oldAppoitments = results.Entities;
            console.log(this.oldAppoitments);
        });
    }


    sendMessage()
	{
		this.messagesService.saveMessage(this.selectedUserId, this.newMessage).subscribe(result => 
			{
				this.newMessage = "";	
			});
		console.log('newmessage value:' + this.newMessage);
    }
    
    selectUser(userId:number)
    {
        this.selectedUserId = userId;
    }

    acceptAppoitment(appoitmentId:number)
    {
      if(confirm("Esit sigur că accepți această programare ?")) {
          this.appoitmentsService.acceptByTeacher(appoitmentId).subscribe((results:any) => {
            this.loadAppoitments();
        });
      }
    }

    selectPayment(appoitmentId:number, price:number, button:any)
    {
      if (button.tagName != "BUTTON")
      {
        button = button.parentElement;
      }
      var item = this.selectedPayment.find(x=>x.AppoitmentId== appoitmentId);

      if (item == null)
      {
        //ADD
        item = new Payment();
        item.AppoitmentId = appoitmentId;
        item.Price = price;

        this.selectedPayment.push(item);

        //updte button
        button.className = button.className.replace('btn-danger','btn-ligth');
        button.innerHTML = "X   Renunta";
        button.style = "color:red";
      }
      else
      {
        //REMOVE
        const index: number = this.selectedPayment.indexOf(item);
        if (index !== -1) {
            this.selectedPayment.splice(index, 1);
        }        
        //updte button
        button.className = button.className.replace('btn-ligth','btn-danger');
        button.innerHTML = "Achita";        
        button.style = "color:white";
      }
      this.calculateTotalToPay();
    }

    pay()
    {
      var lstOfAppoitments = this.selectedPayment.map(function (el) {return el.AppoitmentId} );
      /*if (this.isDebug)
      {
        this.appoitmentsService.payment(lstOfAppoitments).subscribe(result =>
          {
            this.loadAppoitments();
            this.selectedPayment = [];
          });
      }
      else
      {*/
        //TODO: need to implement real payment
        window.location.href = "/payment/" + lstOfAppoitments.join(',');
      //}
    }

    needToPay(appoitment:Appoitment)
    {
      if (!appoitment.AcceptedByTeacher)
      {
        //do not show payment button if teacher not accepted the appoitment 
        return false;
      }
      var isPaid = appoitment.Payment != null && appoitment.Payment.Status == 1;

      return !isPaid;

    }

    calculateTotalToPay()
    {
      let total = 0;
      for (let entry of this.selectedPayment) {
        total += entry.Price;
      }
      this.totalToPay = total;
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
      
      openNewTab(url:string)
      {
        window.open(url, url);
      }

      selectRate(rate:number)
      {
        this.selectedRate = rate;
      }

      saveRate()
      {
        let localModalRef = this.modalRef;
        if (this.selectedRate > -1 && this.selectedAppoitment > -1)
        {
          /*this.appoitmentsService.saveTeacherRating(this.selectedAppoitment, this.selectedRate).subscribe(result =>
            {
              this.loadOldAppoitments();
              this.selectedRate = -1;
              this.selectedAppoitment =-1;
              localModalRef.close();
            });*/
        }
      }
      selectAppoitment(appoitmentId:number)
      {
        this.selectedAppoitment = appoitmentId;
      }
}

interface Appoitment
{
	StartDate: Date,
	EndDate: Date,
	
	Added: string,
	isRead: boolean,
  SenderName: string,

  AcceptedByTeacher: boolean,
  
  Payment: Payment
}

interface AppoitmentChat
{
  AppoitmentId:number,
  UserId:number,
  Message:string,
  Added: Date,
  IsMine:boolean,
  User:User
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

