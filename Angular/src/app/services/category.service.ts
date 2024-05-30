import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CategoryReturnModel } from './types';
import { Observable } from 'rxjs';
import { environment } from '../environment';

export interface ICategoryService {
  createAdmin(name: string): Observable<CategoryReturnModel>;
}

const BASE_URL = environment.apiPrefix;
@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  constructor(private http: HttpClient) { }

  createCategory(name: string): Observable<CategoryReturnModel>{
    return this.http.post<any>(`${BASE_URL}/api/categories`, {
      name
    });
  }
}
