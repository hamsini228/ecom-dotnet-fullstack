import { Component, inject } from '@angular/core';
import { CategoryForm } from '../../models/category-form';
import { ReactiveFormsModule } from "@angular/forms";
import { CategoryService } from '../../services/category-service';

import { Category } from '../../models/category';
import { Router } from '@angular/router';


@Component({
  selector: 'bajaj-register-category',
  imports: [ReactiveFormsModule],
  templateUrl: './register-category.html',
  styleUrl: './register-category.css',
})
export class RegisterCategory {
  
  private _cartegoryService = inject(CategoryService)
  private _router = inject(Router);

  title: string = "Register new Category";
  category: CategoryForm = new CategoryForm();

  onCategorySubmit() {
    const newCategory: Category = {
      categoryId: 0,
      categoryName: this.category.categoryForm.value.categoryName || '',
      description: this.category.categoryForm.value.description || ''
    };

    this._cartegoryService.createCategory(newCategory).subscribe({
      next: () => {
        this._router.navigate(['/categories']);
      },
      error: (err) => {
        console.error('Failed to create category', err);
      }
    });
  }
}
