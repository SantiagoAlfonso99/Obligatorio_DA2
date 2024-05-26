import { Component } from '@angular/core';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css'],
})
export class ProjectComponent {
  pdfs = [
    {
      thumbnailUrl: '../../../assets/Obl1.png',
      url: '../../../assets/DA2Obl1.pdf',
      title: 'Obligatorio 1',
      color: 'color1',
    },
    {
      thumbnailUrl: '../../../assets/Obl2.png',
      url: '../../../assets/DA2Obl2.pdf',
      title: 'Obligatorio 2',
      color: 'color2',
    },
  ];

  openPDF(url: string): void {
    window.open(url, '_blank');
  }
}
