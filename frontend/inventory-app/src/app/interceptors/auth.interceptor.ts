import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
   const token = localStorage.getItem('authToken');

   const authReq = token
      ? req.clone({
           headers: req.headers.set('Authorization', `Bearer ${token}`),
        })
      : req;

   return next(authReq); // NOT next.handle(...)
};
