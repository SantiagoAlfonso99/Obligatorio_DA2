import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { SessionStorageService } from 'src/app/services/session-storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  logoUrl: string = '';
  statusMessage: string = '';
  email: string = '';
  password: string = '';
  showPassword: boolean = false;

  constructor(
    private router: Router,
    private authService: AuthService,
    private _sessionStorageService: SessionStorageService
  ) {}

  ngOnInit(): void {
    this.logoUrl = 'https://avatars.githubusercontent.com/u/124091983';
  }

  login() {
    this.authService.login(this.email, this.password).subscribe((response) => {
      console.log('response', response.token);
      this._sessionStorageService.setToken(response.token);
      this.router.navigate(['/home']);
    });
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }
}
