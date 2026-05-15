import { Product } from "../../products/models/product";
import { CartItems } from "./cart-items";

export interface CartItemDetail {
    cartItem: CartItems;
  product: Product;
}
