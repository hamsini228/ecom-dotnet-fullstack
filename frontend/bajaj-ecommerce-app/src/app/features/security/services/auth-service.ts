import { Injectable,inject } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { AuthResponse } from '../models/auth-response';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _httpCLient =inject(HttpClient);
  private _serviceUrl:string ="https://localhost:7274/api/v1/Security/Login"
  checkCredentials(user:User):Observable<AuthResponse>{
    return this._httpCLient.post<AuthResponse>(this._serviceUrl,user,{
      headers:{
        "Content-Type":"application/json",
      },
    });
  }

}
