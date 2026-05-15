import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { CategoryService } from '../../services/category-service';
import { BehaviorSubject, Observable, switchMap } from 'rxjs';
import { Category } from '../../models/category';
import { CommonModule } from '@angular/common';
import { CategoryDetails } from '../../../components/category-details/category-details';
import { Router } from '@angular/router';
import { getRole } from '../../../../shared/utitilities/auth-utilities';

@Component({
  selector: 'bajaj-categories-list',
  imports: [CommonModule, CategoryDetails],
  templateUrl: './categories-list.html',
  styleUrl: './categories-list.css',
})
export class CategoriesList {
  
  private _categoryService = inject(CategoryService);
  private _router = inject(Router);

  private _refresh$ = new BehaviorSubject<void>(undefined);

  title: string = "Welcome to Categories list";
  selctedCategoryId: number;
  isAdmin:boolean =getRole() ==="Admin";

  categories$ = this._refresh$.pipe(
    switchMap(() => this._categoryService.getAllCategories())
  );
  
  showProducts(categoryId: number) {
    this._router.navigate(['/products'], { queryParams: { categoryId } });
  }

  onSelect(categoryId: number): void {
    this.selctedCategoryId = categoryId;
  }
  createCategory(){
    this._router.navigate(['/categories/register']);
  }

  deleteCategory(categoryId: number): void {
    const confirmed = confirm('Are you sure you want to delete this category?');
    if (confirmed) {
      this._categoryService.deleteCategory(categoryId).subscribe({
        next: () => {
          this._refresh$.next()
        },
        error: (err) => console.error('Delete failed', err),
      });
    }
  }
}
