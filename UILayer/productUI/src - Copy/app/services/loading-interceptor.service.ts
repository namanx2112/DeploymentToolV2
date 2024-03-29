import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { LoadingService } from 'src/app/services/loading.service';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class LoadingInterceptorService {

  activeRequests: number = 0;

    constructor(
        private loadingScreenService: LoadingService,
        private authService: AuthService
    ) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        if (this.activeRequests === 0) {
            this.loadingScreenService.startLoading();
        }

        this.activeRequests++;

        return next.handle(request).pipe(
            finalize(() => {
                this.activeRequests--;
                if (this.activeRequests === 0) {
                    this.loadingScreenService.stopLoading();
                }
            }),
            catchError(err => {
                if (err.status === 401 && err.url.indexOf("Logout") == -1) {
                    this.authService.loggedOut(false);
                }
                const error = err.error.message || err.statusText;
                    return throwError(error);
            })
        )
    };
}