import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { tokenCheckerGuard } from './token-checker-guard';

describe('tokenCheckerGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) =>
    TestBed.runInInjectionContext(() => tokenCheckerGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
