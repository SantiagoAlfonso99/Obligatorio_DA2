import { Injectable } from '@angular/core';
import { environment } from '../environment';
import { Observable } from 'rxjs';
import { AdminReturnModel } from './types';
import { HttpClient } from '@angular/common/http';

const BASE_URL = environment.apiPrefix;

export interface IAdminService {
  createAdmin( email: string, password: string, name: string, lastName : string ): Observable<AdminReturnModel>;
}

@Injectable({
  providedIn: 'root'
})
export class AdminService implements IAdminService{

  constructor(private http: HttpClient) { }

  createAdmin( email: string, password: string, name: string, lastName : string ): Observable<AdminReturnModel>{
    console.log('Email', email);
    console.log('LastName', lastName);
    console.log('Name', name);
    console.log('Password', password);
    return this.http.post<any>(`${BASE_URL}/api/admins`, {
      name,
      lastName,
      password,
      email
    });
  }
}
