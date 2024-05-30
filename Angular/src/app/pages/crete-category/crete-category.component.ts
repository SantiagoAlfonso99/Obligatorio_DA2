import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/services/admin.service';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-crete-category',
  templateUrl: './crete-category.component.html',
  styleUrls: ['./crete-category.component.css']
})
export class CreteCategoryComponent {
  name :string = '';

  constructor(
    private router: Router,
    private categoryService: CategoryService
  ) {}

  createCategory() {
    this.categoryService.createCategory(this.name).subscribe((response) => {
      this.router.navigate(['/home']);
    });
  }
}
