import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environment';
import { Observable } from 'rxjs';

export interface IInvitationService {
  createInvitation( email: string, name: string, deadLine : Date, role: string ): Observable<any>;
}

const BASE_URL = environment.apiPrefix;
@Injectable({
  providedIn: 'root'
})
export class InvitationService implements IInvitationService{

  constructor(private http: HttpClient) { }

  createInvitation( email: string, name: string, deadLine : Date, role: string ): Observable<any>{
    return this.http.post<any>(`${BASE_URL}/api/invitations`, {
      email,
      name,
      deadLine,
      role
    });
  }
}
