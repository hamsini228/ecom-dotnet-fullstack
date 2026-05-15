import { Component, inject } from '@angular/core';
import { CartService } from '../../services/cart-service';
import { ProductService } from '../../../products/services/product-service';
import { CartItemDetail } from '../../models/cart-item-detail';
import { forkJoin, map, Observable, of, switchMap, take } from 'rxjs';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { InvoiceService } from '../../../invoices/services/invoice-service';
import { PaymentService } from '../../services/payment-service';

@Component({
  selector: 'bajaj-your-cart',
  imports: [RouterLink, CommonModule],
  templateUrl: './your-cart.html',
  styleUrl: './your-cart.css',
})
export class YourCart {
  private _cartService = inject(CartService);
  private _productService = inject(ProductService);
  private _router = inject(Router);
  private _invocieService =inject(InvoiceService);
  private _paymentService =inject(PaymentService);

  cartItems$!: Observable<CartItemDetail[]>;

  ngOnInit() {
    const cartId = this._cartService.storedCartId;

    if (!cartId) {
      this.cartItems$ = of([]);
      return;
    }

    this.cartItems$ = this._cartService.getCartItemsByCartId(cartId).pipe(
      switchMap((items) => {
        if (items.length === 0) return of([]);
        return forkJoin(
          items.map((item) =>
            this._productService
              .getById(item.productId)
              .pipe(map((product) => ({ cartItem: item, product }) as CartItemDetail)),
          ),
        );
      }),
    );
  }

  grandTotal(details: CartItemDetail[]): number {
    return details.reduce((sum, d) => {
      return (
        sum +
        (d.product.unitPrice - (d.product.unitPrice * d.product.discount) / 100) *
          d.cartItem.quantity
      );
    }, 0);
  }

  removeItem(cartItemId: number, details: CartItemDetail[]) {
    this._cartService.deleteCartItem(cartItemId).subscribe({
      next: () => {
        const cartId = this._cartService.storedCartId;

        if (!cartId) {
          this.cartItems$ = of([]);
          return;
        }

        this.cartItems$ = this._cartService.getCartItemsByCartId(cartId).pipe(
          switchMap((items) => {
            if (items.length === 0) {
              this._cartService.clearLocalCart();
              return of([]);
            }
            return forkJoin(
              items.map((item) =>
                this._productService.getById(item.productId).pipe(
                  map(
                    (product) =>
                      ({
                        cartItem: item,
                        product,
                      }) as CartItemDetail,
                  ),
                ),
              ),
            );
          }),
        );
      },

      error: (err) => {
        console.error('Remove Item Error:', err);
      },
    });
  }

  updateQuantity(detail: CartItemDetail) {
    const updated = { ...detail.cartItem };
    this._cartService.updateCartItem(updated).subscribe();
  }

  

isLoading = false;

onCheckout() {
  const cartId = this._cartService.storedCartId!;

  this._paymentService.createOrder(cartId).subscribe({
    next: (order) => {
       console.log('Order:', order); 
      this._paymentService.openRazorpay(order, cartId, (invoiceId) => {
        // ✅ routing in component
        this._cartService.clearLocalCart();
        this._router.navigate(['/invoice', invoiceId]); // → invoice-detail page
      });
    },
    error: (err) => console.error('Order creation failed', err)
  });
}
}
