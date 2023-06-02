import { HttpClient, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {map} from 'rxjs/operators';
import { User } from '../_models/user';
import { Observable, ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService{
baseUrl = 'https://localhost:5266/api/';

token: string;

  constructor(private http: HttpClient) { }

  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  login(model: any)
  {
   return this.http.post(this.baseUrl + 'account/login', model).pipe(
    map((response: User) => {
      const user = response;
      if(user)
      {
        localStorage.setItem('user', JSON.stringify(user));
        this.currentUserSource.next(user);
        this.token = user.token;
      }
    })
   ) 
  }

  register(model: any){
return this.http.post(this.baseUrl+'account/register', model).pipe(
  map((user: User) => {
    if (user)
    {
      localStorage.setItem('user', JSON.stringify(user));
      this.currentUserSource.next(user);
    }
  })
);
  }

setCurrentUser(user: User)
{
  this.currentUserSource.next(user);
  this.token = user.token;
}


  logout()
  {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
