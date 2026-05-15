// import { Component, inject, Input, SimpleChanges } from '@angular/core';

import { ProductService } from '../../products/services/product-service';
import { Product } from '../../products/models/product';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Observable, switchMap, take } from 'rxjs';
import { CartService } from '../../carts/services/cart-service';

@Component({
  selector: 'bajaj-product-details',
  imports: [CommonModule],
  templateUrl: './product-details.html',
  styleUrl: './product-details.css',
})
export class ProductDetails implements OnInit {
  private _productService = inject(ProductService);
  private _activedRoute = inject(ActivatedRoute);
  private _cartService = inject(CartService);
  private _router = inject(Router);

  product$: Observable<Product>;

  ngOnInit(): void {
    let productId = Number.parseInt(this._activedRoute.snapshot.params['id']);
    this.product$ = this._productService.getById(productId);
  }

  onAddToCart(product: Product) {
    this._cartService.addToCart(product).subscribe({
      next: (res) => {
        console.log('Added Successfully:', res);
        this._router.navigate(['/cart']);
      },
      error: (err) => {
        console.error('Add To Cart Error:', err);
      },
    });
  }
}
