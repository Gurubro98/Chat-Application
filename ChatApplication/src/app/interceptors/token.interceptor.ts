import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(
    private auth: AuthService,
    private router: Router,
    private messageService: MessageService
  ) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    const token = this.auth.getToken();
    if (token) {
      request = request.clone({
        setHeaders: { Authorization: `Bearer ${token}` },
      });
    }

    return next.handle(request).pipe(
      catchError((err: any) => {
        if (err.statusText == 'Unknown Error') {
          this.messageService.add({
            severity: 'error',
            summary: 'Success',
            detail: 'Internal Error',
          });
        } else {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: err.error.message,
          });
        }
        if (err instanceof HttpErrorResponse) {
          console.log(err.status);
          if (err.status === 401) {
            this.router.navigate(['login']);
          }
        }
        debugger
        return throwError(() => new Error(err.error.message));
      })
    );
  }
}
