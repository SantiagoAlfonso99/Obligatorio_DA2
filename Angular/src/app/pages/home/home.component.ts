// home.component.ts

import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  sidebarOpened: boolean = true;

  toggleSidebar() {
    this.sidebarOpened = !this.sidebarOpened;
  }
}
