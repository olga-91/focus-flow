import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpHeaders,
  HttpInterceptor, HttpInterceptorFn,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import {environment} from '../environment';
import {ToastService} from './toast.service';

@Injectable()
export class AuthInterceptorService implements HttpInterceptor {
  constructor(private toastService: ToastService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (req.url.startsWith(environment.webApiUrl)) {
      const token = localStorage.getItem('token');
      if (token) {
        const headers = new HttpHeaders().set(
          'Authorization',
          `Bearer ${token}`
        );
        const authReq = req.clone({ headers });
        return next.handle(authReq).pipe(
          tap(
            (_) => {},
            (error) => {
              const respError = error as HttpErrorResponse;
              if (
                respError &&
                (respError.status === 401 || respError.status === 403)
              ) {
                this.toastService.error("Unauthorized");
              }
            }
          )
        );
      } else {
        return next.handle(req);
      }
    } else {
      return next.handle(req);
    }
  }
}
