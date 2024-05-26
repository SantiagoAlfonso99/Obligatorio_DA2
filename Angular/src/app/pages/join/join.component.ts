import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-join',
  templateUrl: './join.component.html',
  styleUrls: ['./join.component.css'],
})
export class JoinComponent implements OnInit {
  joinForm!: FormGroup;
  statusMessage: string = '';
  logoUrl: string = '';

  constructor(private fb: FormBuilder) {
    this.createForm();
  }
  ngOnInit(): void {
    this.logoUrl = 'https://avatars.githubusercontent.com/u/124091983';
  }

  createForm() {
    this.joinForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      acceptInvitation: [false, Validators.requiredTrue],
    });
  }

  onJoin() {
    if (this.joinForm.valid) {
      // Perform join operation
      console.log('Form Data:', this.joinForm.value);
      this.statusMessage = 'Registration successful!';
      // Further logic to handle registration
    } else {
      this.statusMessage = 'Please fill in all required fields correctly!';
    }
  }
}
