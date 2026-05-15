import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../models/category';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private _httpClient=inject(HttpClient);
  private _apiUrl ="https://localhost:7274/api/v1/Categories"
  
  
    getAllCategories(): Observable<Category[]> {
    return this._httpClient.get<Category[]>(`${this._apiUrl}?t=${Date.now()}`);
  }
   getById(id: number): Observable<Category> {
    return this._httpClient.get<Category>(`${this._apiUrl}/${id}`);
  }

  createCategory(category: { categoryName: string; description: string }): Observable<Category> {
    console.log("this is service");
    console.log(category);
    return this._httpClient.post<Category>(this._apiUrl, category);
  }


  updateCategory(category:Category): Observable<void> {
    return this._httpClient.put<void>(this._apiUrl, category);
  }

  deleteCategory(id: number): Observable<void> {
    return this._httpClient.delete<void>(`${this._apiUrl}/${id}`);
  }
}
