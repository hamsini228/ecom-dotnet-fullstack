import { HttpInterceptorFn } from '@angular/common/http';
import { getToken } from '../../shared/utitilities/auth-utilities';

export const tokenInterceptor: HttpInterceptorFn = (req, next) => {
  if(!req.url.includes('security')){
    const headers =req.headers
                      .set('Content-Type','application/json')
                      .set('Authorization',`Bearer ${getToken()}`);
    const authReq =req.clone({headers});
    return next(authReq);
  }
  return next(req);
};

