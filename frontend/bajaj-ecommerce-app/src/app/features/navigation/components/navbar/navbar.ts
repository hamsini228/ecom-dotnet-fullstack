import { Component, inject, signal } from '@angular/core';
import { NavigationStart, Router, RouterLink, RouterLinkActive } from "@angular/router";
import { getRole, getToken, removeAuthInformation } from '../../../../shared/utitilities/auth-utilities';

@Component({
  selector: 'bajaj-navbar',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar {
   private _router = inject(Router);
  isLoggedIn = signal<boolean>(getToken() ? true : false);
  private _token: string|null;
  role=signal<string>('');


  ngOnInit(): void {
    this._router.events.subscribe((event) => {
      this._token = getToken();
      if (event instanceof NavigationStart) {
        if (this._token) {
          this.isLoggedIn.set(true);
          this.role.set(getRole()!)
        } else {
          this.isLoggedIn.set(false);
        }
      }
    });
  }

  logout(): void {
    this.isLoggedIn.set(false);
    removeAuthInformation();
    this._router.navigate(['/home']);
  }

}
