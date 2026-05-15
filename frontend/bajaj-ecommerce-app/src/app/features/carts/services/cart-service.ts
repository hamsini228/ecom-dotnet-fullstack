import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Cart } from '../models/cart';
import { CartItems } from '../models/cart-items';
import { Product } from '../../products/models/product';
import { concatMap, tap,map, catchError, of } from 'rxjs';
import { CreateCartDto } from '../models/create-cart-dto';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private _httpClient=inject(HttpClient);
  private _apiUrl ="https://localhost:7274/api/v1";

  private readonly CART_ID_KEY = 'cartId';

  public get customerId(): number {
    const val = localStorage.getItem('customerId');
    return val ? +val : 0;
}

  public get storedCartId(): number | null {
    const val = localStorage.getItem(this.CART_ID_KEY);
    return val ? +val : null;
  }
  private buildCartItem(cartId: number, product: Product): CartItems {
    return {
      cartItemId: 0,
      cartId,
      productId: product.productId,
      quantity: 1,

    };
  }
  getCartItemsByCartId(cartId: number) {
    if(!cartId)return of([]);
  return this._httpClient.get<CartItems[]>(`${this._apiUrl}/CartItems`).pipe(
    map((items) => items.filter((item) => item.cartId === cartId))
  );
}
 addToCart(product: Product) {
  const existingCartId = this.storedCartId;

  
  if (existingCartId) {
    return this.getCartById(existingCartId).pipe(
      concatMap(() => {
        const item = this.buildCartItem(existingCartId, product);
        return this.addCartItem(item);
      }),
      catchError(() => {
        this.clearLocalCart();
        return this.createNewCartAndAdd(product);
      })
    );
  }

  // CASE 2: No cart found
  return this.createNewCartAndAdd(product);
}
private createNewCartAndAdd(product: Product) {
  return this.createCart(this.customerId).pipe(
    tap((res: Cart) => {
      console.log('Created Cart:', res);

      if (!res || !res.cartId) {
        throw new Error('Cart creation failed: cartId missing');
      }

      localStorage.setItem(this.CART_ID_KEY, res.cartId.toString());
    }),
    concatMap((res: Cart) => {
      const item = this.buildCartItem(res.cartId, product);
      return this.addCartItem(item);
    })
  );
}

  createCart(customerId: number) {
  const payload: CreateCartDto = {
    customerId,
    cartDate: new Date()
  };

  return this._httpClient.post<Cart>(`${this._apiUrl}/Carts`, payload);
}

  addCartItem(cartItem: CartItems) {
    return this._httpClient.post(`${this._apiUrl}/CartItems`, cartItem);
  }

  getCartById(cartId: number) {
    return this._httpClient.get<any>(`${this._apiUrl}/Carts/${cartId}`);
  }

  updateCartItem(cartItem: CartItems) {
    return this._httpClient.put(`${this._apiUrl}/CartItems`, cartItem);
  }

  deleteCartItem(cartItemId: number) {
    return this._httpClient.delete(`${this._apiUrl}/CartItems/${cartItemId}`);
  }
  clearLocalCart() {
    localStorage.removeItem(this.CART_ID_KEY);
  }
}
