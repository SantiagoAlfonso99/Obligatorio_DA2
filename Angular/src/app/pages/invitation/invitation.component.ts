import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { InvitationService } from 'src/app/services/invitation.service';

@Component({
  selector: 'app-invitation',
  templateUrl: './invitation.component.html',
  styleUrls: ['./invitation.component.css']
})
export class InvitationComponent {
  name :string = '';
  email :string = '';
  deadLine :Date = new Date();
  role :string = '';
  
  roles :string[] = ['Manager','CompanyAdmin']

  constructor(
    private router: Router,
    private invitationService: InvitationService
  ) {}

  createCategory() {
    this.invitationService.createInvitation(this.email, this.name, this.deadLine, this.role).subscribe((response) => {
      this.router.navigate(['/home']);
    });
  }
}
