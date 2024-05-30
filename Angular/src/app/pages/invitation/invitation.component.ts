import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { InvitationService } from 'src/app/services/invitation.service';
import { InvitationReturnModel } from 'src/app/services/types';

@Component({
  selector: 'app-invitation',
  templateUrl: './invitation.component.html',
  styleUrls: ['./invitation.component.css']
})
export class InvitationComponent implements OnInit{
  name :string = '';
  email :string = '';
  deadLine :Date = new Date();
  role :string = '';
  
  roles :string[] = ['Manager','CompanyAdmin'];
  invitations : InvitationReturnModel[] = [];

  constructor(
    private router: Router,
    private invitationService: InvitationService
  ) {}

  ngOnInit(): void {
    this.getAllInvitations();
  }

  createInvitation() {
    this.invitationService.createInvitation(this.email, this.name, this.deadLine, this.role).subscribe((response) => {
      this.router.navigate(['/home']);
    });
  }

  deleteInvitation(invitationId : number) {
    this.invitationService.deleteInvitation(invitationId).subscribe((response) => {
      this.getAllInvitations();
    });
  }

  getAllInvitations() {
    this.invitationService.getAllInvitations().subscribe((response) => {
      this.invitations = response;
      console.log('largo: ', this.invitations.length)
    });
  }
}
