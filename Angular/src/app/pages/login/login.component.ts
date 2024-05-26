import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  logoUrl: string = '';
  statusMessage: string = '';

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService
  ) {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  ngOnInit(): void {
    this.logoUrl = 'https://avatars.githubusercontent.com/u/124091983';
  }

  onLogin() {
    if (this.loginForm.valid) {
      this.authService.login(this.loginForm.value).subscribe(
        (response) => {
          console.log('Login successful', response);
          this.statusMessage = 'Login successful! Redirecting...';
          this.router.navigate(['/home']); // Redirect after successful login
        },
        (error) => {
          console.error('Login failed:', error);
          this.statusMessage =
            'Login failed: ' + (error.error.message || 'Unknown error');
        }
      );
    } else {
      this.statusMessage = 'Please check your inputs and try again.';
    }
  }
}
