import { HttpClient } from '@angular/common/http';
import { Component, OnInit} from '@angular/core';
import { User } from '../_models/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit{
registerMode = false;
users: any;

constructor(private http: HttpClient){}

ngOnInit(): void {
  this.getUsers();
}

registerToggle(){
  this.registerMode = !this.registerMode;
}

getUsers() {
  const user: User = JSON.parse(localStorage.getItem('user'));
  const headers = {'Authorization': `Bearer ${user.token}`};
  this.http.get('https://localhost:5266/api/users', {headers}).subscribe(users => this.users = users);
}

}
