import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable } from 'rxjs';
import { User } from '../_models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
model: any = {}
// currentUser$: Observable<User>;
// loggedIn: boolean = false; -> din cauza lui currentUser$ este pus in comentariu

constructor(public accountService: AccountService) {}

ngOnInit(): void {  
  // this.currentUser$ = this.accountService.currentUser$;
}

login() {
  this.accountService.login(this.model).subscribe(response => {
    console.log(response);
    // this.loggedIn = true;
  }, error => {
    console.log(error);
  });
}

logout() {
  this.accountService.logout();
  // this.loggedIn = false;
}

// getCurrentUser()
// {
//   this.accountService.currentUser$.subscribe(user => {
//     this.loggedIn = !!user;
//   }, error => {
//     console.log(error);
//   });
// }
}
