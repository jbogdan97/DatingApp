import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AccountService } from './_services/account.service';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'The Dating app';
users: any;

  constructor(private accountService: AccountService)
  {

  }

  ngOnInit(): void 
  {
// this.getUsers();
this.setCurrentUser(); 
}

setCurrentUser()
{
const user: User = JSON.parse(localStorage.getItem('user'));
this.accountService.setCurrentUser(user);
}

  // getUsers()
  // {
  //   this.http.get('http://localhost:5266/api/users').subscribe(response => 
  //   {
  //     this.users = response;
  //   }, 
  //   error => 
  //   { 
  //     console.log(error); 
  //   }
  //   );
  // }
}

