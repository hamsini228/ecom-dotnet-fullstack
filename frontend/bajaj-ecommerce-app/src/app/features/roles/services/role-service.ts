import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Role } from '../models/role';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class RoleService {
  private _httpClient=inject(HttpClient);
  private _apiUrl ="https://localhost:7274/api/v1/Roles"

  getAllRoles():Observable<Role[]>{
    return this._httpClient.get<Role[]>(this._apiUrl);
  }

  getById(id: number): Observable<Role> {
      return this._httpClient.get<Role>(`${this._apiUrl}/${id}`);
    }
  
    createRole(role:Role): Observable<Role> {
      return this._httpClient.post<Role>(this._apiUrl, role);
    }
  
  
    updateRole(role:Role): Observable<void> {
      return this._httpClient.put<void>(this._apiUrl, role);
    }
  
    deleteRole(id: number): Observable<void> {
      return this._httpClient.delete<void>(`${this._apiUrl}/${id}`);
    }

}
