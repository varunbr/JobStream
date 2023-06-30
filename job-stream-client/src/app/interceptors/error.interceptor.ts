import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { ToastrService } from '../services/toastr.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private toastr: ToastrService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error) => {
        console.log(error);
        switch (error.status) {
          case 0:
            this.toastr.error('Unable to reach server!');
            break;
          case 400:
            if (error.error.errors) {
              const modelStateErrors = [];
              for (const key in error.error.errors) {
                modelStateErrors.push(error.error.errors[key]);
              }
              throw modelStateErrors.flat();
            } else if (typeof error.error === 'object') {
              this.toastr.error('Bad request');
            } else {
              this.toastr.error(error.error);
            }
            break;
          case 403:
            this.toastr.error('You are forbidden.');
            break;
          case 404:
            this.toastr.error('Not found.');
            break;
          case 500:
            this.toastr.error('Server error.');
            break;
          default:
            this.toastr.error('Something went wrong');
            break;
        }
        return throwError(() => {
          new Error(error);
        });
      })
    );
  }
}
