import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { InvitationService } from 'src/app/services/invitation.service';

@Component({
  selector: 'app-join',
  templateUrl: './join.component.html',
  styleUrls: ['./join.component.css'],
})
export class JoinComponent implements OnInit {
  joinForm!: FormGroup;
  statusMessage: string = '';
  logoUrl: string = '';
  acceptInvitationValue: boolean = false;

  constructor(private fb: FormBuilder, private invitationService: InvitationService) {
    this.createForm();
  }
  ngOnInit(): void {
    this.logoUrl = 'https://avatars.githubusercontent.com/u/124091983';
  }

  createForm() {
    this.joinForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      acceptInvitation: [false],
      invitationId: ['', Validators.required]
    });
  }

  onJoin() {
    if (this.joinForm.valid) {
      const emailValue = this.joinForm.get('email')?.value;
      const passwordValue = this.joinForm.get('password')?.value;
      this.acceptInvitationValue = this.joinForm.get('acceptInvitation')?.value;
      const invitationIdValue = this.joinForm.get('invitationId')?.value;
      this.invitationService.acceptInvitation(invitationIdValue, emailValue, passwordValue, this.acceptInvitationValue).subscribe((response) => {
        this.statusMessage = 'Registration successful!';
        console.log('result', this.acceptInvitationValue);
      });      
    } else {
      this.statusMessage = 'Please fill in all required fields correctly!';
    }
  }
}
