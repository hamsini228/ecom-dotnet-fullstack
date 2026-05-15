import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { roleCheckerGuard } from './role-checker-guard';

describe('roleCheckerGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) =>
    TestBed.runInInjectionContext(() => roleCheckerGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
