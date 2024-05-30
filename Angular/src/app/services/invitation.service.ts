import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environment';
import { Observable } from 'rxjs';
import { InvitationReturnModel } from './types';

export interface IInvitationService {
  createInvitation( email: string, name: string, deadLine : Date, role: string ): Observable<any>;
  getAllInvitations(): Observable<InvitationReturnModel[]>;
  deleteInvitation(invitationId: number): Observable<any>;
  acceptInvitation(invitationId : number,email : string, password : string, response: boolean): Observable<any>;
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

  getAllInvitations(): Observable<InvitationReturnModel[]>{
    return this.http.get<any>(`${BASE_URL}/api/invitations`, {});
  }

  deleteInvitation(invitationId: number): Observable<any>{
    return this.http.delete<any>(`${BASE_URL}/api/invitations/${invitationId}`);
  }

  acceptInvitation(invitationId : number, email : string, password : string, response: boolean): Observable<any>{
    console.log('Respuesta ', response);
    return this.http.put<any>(`${BASE_URL}/api/invitations/${invitationId}`, {
      email,
      password,
      acceptInvitation : response    
    });
  }
}
