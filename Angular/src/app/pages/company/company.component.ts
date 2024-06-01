import { Component } from '@angular/core';
import { CompanyService } from 'src/app/services/company.service';

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.css']
})
export class CompanyComponent {
  name :string = '';
  newName :string = '';
  showUpdateForm :boolean = false;

  constructor(
    private companyService: CompanyService,
  ) {}

  createCompany() {
    this.companyService.createCompany(this.name).subscribe((response) => {
      this.showUpdateForm = true;
    });
  }

  updateCompany() {
    this.companyService.updateCompany(this.newName).subscribe((response) => {
    });
  }
}
