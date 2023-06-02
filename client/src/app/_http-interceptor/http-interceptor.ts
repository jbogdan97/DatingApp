import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable, observable } from 'rxjs';
import { AccountService } from '../_services/account.service';

@Injectable()
export class HeaderInterceptor implements HttpInterceptor {
    constructor(private accountService: AccountService) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const authToken = this.accountService.token;

        const headers = req.headers
            .set('Authorization', `Bearer ${authToken}`);
        const authReq = req.clone({ headers });
        return next.handle(authReq);
    }
}