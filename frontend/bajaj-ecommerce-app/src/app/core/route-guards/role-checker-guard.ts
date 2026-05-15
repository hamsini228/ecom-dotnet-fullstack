import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

import { getRole } from '../../shared/utitilities/auth-utilities';

export const roleCheckerGuard: CanActivateFn = (route, state) => {
  const _router =inject(Router);
  let role =getRole();
  const requires =route.data['roles'] as string[];
  if(role && requires.includes(role) ){
    return true;
  }
  return false;
};
