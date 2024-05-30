import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpInterceptor,
  HttpEvent,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { SessionStorageService } from './session-storage.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private sessionStorageService: SessionStorageService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const token = this.sessionStorageService.getToken();

    let newRequest = request;
    if (request.url.includes('session')) {
      return next.handle(request);
    }

    
    if (!!token) {
      const headers = request.headers.set('Authorization', token);
      newRequest = request.clone({ headers });
    }
    return next.handle(newRequest);
  }
}
