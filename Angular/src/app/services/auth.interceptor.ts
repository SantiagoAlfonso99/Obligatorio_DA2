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
    console.log('RESPONSE', token)
    if (request.url.includes('login')) {
      return next.handle(request);
    }

    if (!!token) {
      const headers = request.headers.set('Authorization', 'D503C1D5-C876-4FC1-B77B-D1925ABE410F');
      newRequest = request.clone({ headers });
    }
    console.log('response', token)
    return next.handle(newRequest);
  }
}
