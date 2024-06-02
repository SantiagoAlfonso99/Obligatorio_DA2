import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environment';
import { BuildingReturnModel } from './types';

export interface IBuildingService {
  createBuilding(name: string, address: string, latitude: number, longitude: number, commonExpenses: number): Observable<any>;
  getAllBuildings(): Observable<BuildingReturnModel[]>;
  modifyBuildingManager(managerId: number, buildingId : number): Observable<any>;
}

const BASE_URL = environment.apiPrefix;

@Injectable({
  providedIn: 'root'
})
export class BuildingService {

  constructor(private http: HttpClient) { }

  createBuilding(name: string, address: string, latitude: number, longitude: number, commonExpenses: number): Observable<any>{
    return this.http.post<any>(`${BASE_URL}/api/buildings`, {
      name,
      address,
      latitude,
      longitude
    });
  }

  getAllBuildings(): Observable<BuildingReturnModel[]>{
    return this.http.get<any>(`${BASE_URL}/api/buildings`);
  }

  modifyBuildingManager(managerId: number, buildingId : number): Observable<any>{
    return this.http.put<any>(`${BASE_URL}/api/buildings/${buildingId}/manager`, {
      managerId
    });
  }
}
