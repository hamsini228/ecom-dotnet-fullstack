import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private _httpClient = inject(HttpClient);
  private _baseUrl = 'https://localhost:7274/api/v1/products';


  getAllProducts(): Observable<Product[]> {
    return this._httpClient.get<Product[]>(this._baseUrl);
  }

  getById(id: number): Observable<Product> {
    return this._httpClient.get<Product>(`${this._baseUrl}/${id}`);
  }

  createProduct(product:Product): Observable<Product> {
    return this._httpClient.post<Product>(this._baseUrl, product);
  }

  updateProduct(product:Product): Observable<void> {
    return this._httpClient.put<void>(this._baseUrl, product);
  }

  
  deleteProduct(id:number): Observable<void> {
    return this._httpClient.delete<void>(`${this._baseUrl}/${id}`);
  }
}
