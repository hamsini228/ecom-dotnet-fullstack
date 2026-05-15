import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  private _httpClient = inject(HttpClient);
  private _serviceUrl: string = "https://localhost:7274/api/v1/Customers";

  getCustomers(): Observable<Customer[]> {
    return this._httpClient.get<Customer[]>(this._serviceUrl);
  }

  getCustomerById(id: number): Observable<Customer> {
    return this._httpClient.get<Customer>(`${this._serviceUrl}/${id}`);
  }

  getCustomerByUserId(userId: number): Observable<Customer> {
    return this._httpClient.get<Customer>(`${this._serviceUrl}/user/${userId}`);
  }
}
