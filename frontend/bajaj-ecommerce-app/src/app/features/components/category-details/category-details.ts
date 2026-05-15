import { Component, Input, OnChanges, SimpleChanges, inject } from '@angular/core';
import { Category } from '../../categories/models/category';
import { CategoryService } from '../../categories/services/category-service';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'bajaj-category-details',
  imports: [CommonModule],
  templateUrl: './category-details.html',
  styleUrl: './category-details.css',
})
export class CategoryDetails {
  private _categoryService = inject(CategoryService);
  @Input() categoryId: number = 0;
  title:string="Details Of - ";
  category$?: Observable<Category>; // Change to an Observable

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['categoryId'] && this.categoryId > 0) {
      this.category$ = this._categoryService.getById(this.categoryId);
    }
  }
}
