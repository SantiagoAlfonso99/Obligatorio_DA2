import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { JoinComponent } from './pages/join/join.component';
import { TeamComponent } from './pages/team/team.component';
import { ProjectComponent } from './pages/project/project.component';
import { CreateAdminComponent } from './pages/create-admin/create-admin.component';
import { CreteCategoryComponent } from './pages/crete-category/crete-category.component';
import { InvitationComponent } from './pages/invitation/invitation.component';
import { CompanyComponent } from './pages/company/company.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'join', component: JoinComponent },
  { path: 'team', component: TeamComponent },
  { path: 'project', component: ProjectComponent },
  { path: 'admin/createAdmin', component: CreateAdminComponent },
  { path: 'admin/createCategory', component: CreteCategoryComponent },
  { path: 'admin/createInvitation', component: InvitationComponent },
  { path: 'admin/company', component: CompanyComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' }, 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
