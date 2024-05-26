import { Component } from '@angular/core';

interface TeamMember {
  name: string;
  role: string;
  imageUrl: string;
  text: string;
}

@Component({
  selector: 'app-team',
  templateUrl: './team.component.html',
  styleUrls: ['./team.component.css'],
})
export class TeamComponent {
  teamMembers: TeamMember[] = [
    {
      name: 'Gonzalo Loureiro',
      role: 'Student - Developer',
      imageUrl: '../../../assets/sample_people.png',
      text: 'Glavi amet ritnisl libero molestie ante ut fringilla purus eros quis glavrid from dolor amet iquam lorem bibendum',
    },
    {
      name: 'Santiago Alfonso',
      role: 'Student - Developer',
      imageUrl: '../../../assets/sample_people.png',
      text: 'Glavi amet ritnisl libero molestie ante ut fringilla purus eros quis glavrid from dolor amet iquam lorem bibendum',
    },
  ];
}
