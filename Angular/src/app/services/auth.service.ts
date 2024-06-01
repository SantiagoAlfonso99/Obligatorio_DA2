import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environment';
import { SessionReturnModel } from './types';

export interface IAuthService {
  login( email: string, password: string ): Observable<SessionReturnModel>;
}

const BASE_URL = environment.apiPrefix;
@Injectable({
  providedIn: 'root',
})
export class AuthService implements IAuthService{
  private apiUrl = '${environment.apiPrefix}/login';

  constructor(private http: HttpClient) {}

  login(email: string, password: string ): Observable<SessionReturnModel> {
    return this.http.post<any>(`${BASE_URL}/api/session`, {
      email,
      password
    });
  }
}
