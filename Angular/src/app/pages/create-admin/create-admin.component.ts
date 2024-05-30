import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/services/admin.service';
import { SessionStorageService } from 'src/app/services/session-storage.service';

@Component({
  selector: 'app-create-admin',
  templateUrl: './create-admin.component.html',
  styleUrls: ['./create-admin.component.css']
})
export class CreateAdminComponent {
  logoUrl: string = '';
  statusMessage: string = '';
  email: string = '';
  password: string = '';
  name :string = '';
  lastName : string = '';
  showPassword: boolean = false;

  constructor(
    private router: Router,
    private adminService: AdminService,
  ) {}

  createAdmin() {
    this.adminService.createAdmin(this.email, this.password, this.name, this.lastName).subscribe((response) => {
      this.router.navigate(['/home']);
    });
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }
}
