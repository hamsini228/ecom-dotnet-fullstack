import { Component, signal } from '@angular/core';
import { Navbar } from "./features/navigation/components/navbar/navbar";
import { RouterModule } from '@angular/router';
import { Footer } from './features/navigation/components/footer/footer';



@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
  imports: [Navbar,RouterModule,Footer]
})
export class App {
  protected readonly title = signal('bajaj-ecommerce-app');
}
