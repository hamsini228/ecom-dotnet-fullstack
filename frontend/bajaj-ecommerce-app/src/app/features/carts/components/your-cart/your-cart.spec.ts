import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YourCart } from './your-cart';

describe('YourCart', () => {
  let component: YourCart;
  let fixture: ComponentFixture<YourCart>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [YourCart],
    }).compileComponents();

    fixture = TestBed.createComponent(YourCart);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
