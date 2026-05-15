import { CanActivateFn } from '@angular/router';
import { inject } from '@angular/core';
import {Router} from '@angular/router'

import { getToken } from '../../shared/utitilities/auth-utilities';

export const tokenCheckerGuard: CanActivateFn = (route, state) => {
  const _router =inject(Router);
  let token =getToken();
  if(!token){
    _router.navigate(['/login'],{
      queryParams:{
        returnUrl:state.url
      }
    })
    return false;
  }
  return true;
};
