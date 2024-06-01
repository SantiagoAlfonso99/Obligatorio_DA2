import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from '../environment';

export interface ICompanyService {
  createCompany(name: string): Observable<any>;
  updateCompany(name: string): Observable<any>;
}

const BASE_URL = environment.apiPrefix;
@Injectable({
  providedIn: 'root'
})
export class CompanyService implements ICompanyService{

  constructor(private http: HttpClient) {}

  createCompany(name: string): Observable<any> {
    return this.http.post<any>(`${BASE_URL}/api/constructionCompanies`, {
      name
    });
  }

  updateCompany(name: string): Observable<any> {
    return this.http.put<any>(`${BASE_URL}/api/constructionCompanies`, {
      name
    });
  }
}
