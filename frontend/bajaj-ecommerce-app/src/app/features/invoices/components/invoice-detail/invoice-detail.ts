import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartService } from '../../../carts/services/cart-service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { InvoiceService } from '../../services/invoice-service';
import { ProductService } from '../../../products/services/product-service';
import { forkJoin, map, switchMap } from 'rxjs';

@Component({
  selector: 'bajaj-invoice-detail',
  imports: [CommonModule, RouterLink],
  templateUrl: './invoice-detail.html',
  styleUrl: './invoice-detail.css',
})
export class InvoiceDetail  {
  private _cartService    = inject(CartService);
  private _productService = inject(ProductService);
  private _invoiceService = inject(InvoiceService);
  private _route          = inject(ActivatedRoute);

  // Single observable — drives the entire template
  invoiceData$ = this._route.paramMap.pipe(
    switchMap(params => {
      const id = Number(params.get('id'));
      return this._invoiceService.getInvoiceById(id);
    }),
    switchMap(invoice =>
      this._cartService.getCartItemsByCartId(invoice.cartId).pipe(
        switchMap(cartItems =>
          forkJoin(
            cartItems.map(cartItem =>
              this._productService.getById(cartItem.productId).pipe(
                map(product => ({ cartItem, product }))
              )
            )
          )
        ),
        map(cartDetails => ({ invoice, cartDetails }))
      )
    )
  );

  // --- Helpers (operate on passed-in data, no class state needed) ---
  originalTotal(details: any[]): number {
    return details.reduce((sum, d) =>
      sum + d.product.unitPrice * d.cartItem.quantity, 0);
  }

  totalSavings(details: any[]): number {
    return details.reduce((sum, d) =>
      sum + (d.product.unitPrice * d.product.discount / 100) * d.cartItem.quantity, 0);
  }

  grandTotal(details: any[]): number {
    return this.originalTotal(details) - this.totalSavings(details);
  }
}
