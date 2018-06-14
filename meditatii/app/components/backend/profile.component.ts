import { Component } from '@angular/core';
import { ProfileService } from '../../services/profile.service';
import { Router, ActivatedRoute } from '@angular/router';
import * as _ from 'underscore';
import {MessageModel} from '../../models/messageModel';
import {ImageCropperComponent,CropperSettings} from 'ng2-img-cropper';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';


@Component({
	moduleId: module.id,
	templateUrl: 'profile.component.html',
	providers: [ProfileService,ImageCropperComponent]  
})
export class ProfileComponent {
	data: any;
	cropperSettings: CropperSettings;
	cropper:ImageCropperComponent;
	closeResult: string;
	editmodeprofile :boolean;
    
	constructor(
		private profileService: ProfileService,
    private router: Router,
		private activateRoute: ActivatedRoute,
		private modalService: NgbModal
    )
    {   
				this.cropperSettings = new CropperSettings();
        this.cropperSettings.width = 100;
        this.cropperSettings.height = 100;
        this.cropperSettings.croppedWidth = 100;
        this.cropperSettings.croppedHeight = 100;
        this.cropperSettings.canvasWidth = 400;
				this.cropperSettings.canvasHeight = 300;
				this.cropperSettings.rounded = true;
    
				this.data = {};

				this.editmodeprofile = false;
	}

	ngOnInit() {
	}

	editProfile()
	{
		this.editmodeprofile = !this.editmodeprofile;
	}

	open(content:any) {
		this.modalService.open(content).result.then((result) => {
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
	
	fileChangeListener(event:any) {
		var image:any = new Image();
		var file:File = event.target.files[0];
		var myReader:FileReader = new FileReader();
		var that = this;
		myReader.onloadend = function (loadEvent:any) {
			image.src = loadEvent.target.result;
			that.cropper.setImage(image);
	
		};
	
		myReader.readAsDataURL(file);
	}

}