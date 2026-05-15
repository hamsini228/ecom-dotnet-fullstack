import { Component ,inject, signal} from '@angular/core';
import { AuthService } from '../../services/auth-service'; 
import { FormsModule } from '@angular/forms';
import { User } from '../../models/user';
import { AuthResponse } from '../../models/auth-response';

import { Router,ActivatedRoute } from '@angular/router';
import { timer } from 'rxjs';
import { setAuthInformation } from '../../../../shared/utitilities/auth-utilities';
import { CustomerService } from '../../../customers/services/customer-service';


@Component({
  selector: 'bajaj-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  title:string="eCommerce Authentication Window"
  private _authService = inject(AuthService);
  private _router = inject(Router);
  private _customerService = inject(CustomerService);
  private _activatedRoute =inject(ActivatedRoute);
  private _returnUrl:string;


  user: User = new User();
  authErrorMessage = signal<string>('');
  authResponse?: AuthResponse;
  ngOnInit():void{
    this._returnUrl=this._activatedRoute.snapshot.queryParams['returnUrl'];
  }
  onCredentialSubmit(): void {
  this._authService.checkCredentials(this.user).subscribe({
    next: (response) => {
      if(response.token){
        this.authResponse=response;
        setAuthInformation(this.authResponse.email,this.authResponse.token,this.authResponse.rollName,this.authResponse.userId.toString());
        this._customerService.getCustomerByUserId(this.authResponse.userId).subscribe({
            next: (customer) => {
              localStorage.setItem('customerId', customer.customerId.toString());
            },
            error: () => {
              console.warn('No customer profile found for this user');
            }
          });
        if(this._returnUrl){
          this._router.navigate([this._returnUrl]);
        }else{
          this._router.navigate(['/home']);
        }
      }else{
        this.authErrorMessage.set(response.message);
        timer(5000).subscribe(() =>{this.authErrorMessage.set('');})
      }
    },
    error: (error) => {
      this.authErrorMessage.set("An Unexpected Error Occured")
    }
  });
}

}
