import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
model: any = {}
// currentUser$: Observable<User>;
// loggedIn: boolean = false; -> din cauza lui currentUser$ este pus in comentariu

constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) {}

ngOnInit(): void {  
  // this.currentUser$ = this.accountService.currentUser$;
}

login() {
  this.accountService.login(this.model).subscribe(response => {
    this.router.navigateByUrl('/members');
    // this.loggedIn = true;
  });
}

logout() {
  this.accountService.logout();
  this.router.navigateByUrl('/');
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
