import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './pages/login/login.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './pages/home/home.component';
import { AuthInterceptor } from './services/auth.interceptor';
import { JoinComponent } from './pages/join/join.component';
import { TeamComponent } from './pages/team/team.component';
import { ProjectComponent } from './pages/project/project.component';
import { SubjectComponent } from './pages/subject/subject.component';
import { SessionStorageService } from './services/session-storage.service';
import { CreateAdminComponent } from './pages/create-admin/create-admin.component';
import { AuthService } from './services/auth.service';
import { AdminService } from './services/admin.service';
import { CreteCategoryComponent } from './pages/crete-category/crete-category.component';
import { CategoryService } from './services/category.service';

@NgModule({
  declarations: [AppComponent, LoginComponent, HomeComponent, JoinComponent, TeamComponent, ProjectComponent, SubjectComponent, CreateAdminComponent, CreteCategoryComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule,
    MatCardModule,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
    MatCheckboxModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
  ],
  providers: [
    SessionStorageService, CategoryService,{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }, AuthService, AdminService
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
