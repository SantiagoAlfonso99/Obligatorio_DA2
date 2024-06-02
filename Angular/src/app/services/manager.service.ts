import { Injectable } from '@angular/core';
import { environment } from '../environment';
import { ManagerReturnModel } from './types';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

const BASE_URL = environment.apiPrefix;

export interface IManagerService {
  getAllManagers(): Observable<ManagerReturnModel>;
}

@Injectable({
  providedIn: 'root'
})
export class ManagerService {

  constructor(private http: HttpClient) { }

  getAllManagers(): Observable<ManagerReturnModel[]>{
    return this.http.get<any>(`${BASE_URL}/api/managers`);
  }
}
