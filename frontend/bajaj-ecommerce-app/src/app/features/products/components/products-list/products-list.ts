import { Component, inject, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../../models/product';
import { ProductService } from '../../services/product-service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';

@Component({
  selector: 'bajaj-products-list',
  imports: [CommonModule,RouterLink,FormsModule],
  templateUrl: './products-list.html',
  styleUrl: './products-list.css',
})
export class ProductsList implements OnInit{
  private _productService =inject(ProductService);
  private _activatedRoute=inject(ActivatedRoute);
  title:string="Welcome to Product list";
  readonly PAGE_SIZE = 6;
  products$:Observable<Product[]>;

  allProducts: Product[] = [];
  filteredProducts: Product[] = [];
  searchTerm: string = '';
  sortOrder: 'asc' | 'desc' | 'none' = 'none';
  currentPage: number = 1;
  isLoaded: boolean = false;
  private _categoryId :number |null=null;

  get pagedProducts(): Product[] {
    const start = (this.currentPage - 1) * this.PAGE_SIZE;
    return this.filteredProducts.slice(start, start + this.PAGE_SIZE);
  }

  get totalPages(): number {
    return Math.ceil(this.filteredProducts.length / this.PAGE_SIZE);
  }

  get pages(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }
  ngOnInit(): void {
     const param = this._activatedRoute.snapshot.queryParams['categoryId'];
    this._categoryId = param ? +param : null;

   
    if (this._categoryId) {
      this.title = `Products in Category ${this._categoryId}`;
    }
    this.products$ = this._productService.getAllProducts();

    this.products$.subscribe({
      next: (products) => {
        this.allProducts = this._categoryId
      ? products.filter(p => p.categoryId === this._categoryId) 
      : products;   
        this.filteredProducts = this.allProducts;
        this.isLoaded = true;
      },
      error: (err) => console.error('Failed to load products', err),
    });
  }  
   applyFilters(): void {
    let result = this.allProducts;

    if (this.searchTerm.trim()) {
      result = result.filter(p =>
        p.productName.toLowerCase().includes(this.searchTerm.trim().toLowerCase())
      );
    }

    if (this.sortOrder === 'asc') {
      result = [...result].sort((a, b) => a.unitPrice - b.unitPrice);
    } else if (this.sortOrder === 'desc') {
      result = [...result].sort((a, b) => b.unitPrice - a.unitPrice);
    }

    this.filteredProducts = result;
    this.currentPage = 1;
  }
   goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages)
      this.currentPage = page;
  }
  // selectedProductId:number;
  // onSelect(productId:number):void{
  //   this.selectedProductId=productId;
  //   console.log(this.selectedProductId);
  // }
}
