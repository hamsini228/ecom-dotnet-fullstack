import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { InvoiceService } from '../../invoices/services/invoice-service';
import Razorpay from 'razorpay';
import { getToken } from '../../../shared/utitilities/auth-utilities';

@Injectable({
  providedIn: 'root',
})
export class PaymentService {

  private _httpClient = inject(HttpClient);
  private _apiUrl = "https://localhost:7274/api/v1";
  private _invoiceService = inject(InvoiceService);


  createOrder(cartId: number) {
    console.log('Token:', getToken());
    return this._httpClient.post<any>(
      `${this._apiUrl}/Carts/create-razorpay-order?cartId=${cartId}`, {}
    );
  }

  openRazorpay(order: any, cartId: number, onSuccess: (invoiceId: number) => void) {
    const options :any ={
      key: order.key,
      amount: order.amount,
      currency: order.currency,
      name: 'My Shoe World',
      description: 'Order Payment',
      order_id: order.orderId,
      handler: (response: any) => {
        // build payload matching your CreateInvoicePayload interface
        const payload = {
          invoiceDate: new Date().toISOString(),
          cartId: cartId,
          paymentId: response.razorpay_payment_id,  // from Razorpay
          orderId: response.razorpay_order_id        // from Razorpay
        };

        this._invoiceService.createInvoice(payload).subscribe({
          next: (invoice) => {
            onSuccess(invoice.invoiceId);
          },
          error: (err) => {
            console.error('Invoice creation failed', err);
          }
        });
      },
      theme: { color: '#3399cc' }
    };

    const rzp = new (window as any).Razorpay(options);
    rzp.open();
  }
}
