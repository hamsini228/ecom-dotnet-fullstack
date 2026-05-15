import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { CreateInvoicePayload } from '../models/create-invoice-payload';
import { Observable } from 'rxjs';
import { Invoice } from '../models/invoice';

@Injectable({
  providedIn: 'root',
})
export class InvoiceService {
  private apiUrl = 'https://localhost:7274/api/v1/Invoices';

  private _httpClient =inject(HttpClient);

  createInvoice(payload: CreateInvoicePayload): Observable<Invoice> {
    return this._httpClient.post<Invoice>(`${this.apiUrl}`, payload);
  }

  getInvoiceById(id: number): Observable<Invoice> {
    return this._httpClient.get<Invoice>(`${this.apiUrl}/${id}`);
  }
}
