import { Component, OnInit } from '@angular/core';
import { Observable, take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/user';
import { UserParams } from 'src/app/_models/userParams';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit{
members: Member[];
pagination: Pagination;
userParams: UserParams;
user: User;
genderList = [{value: 'male', display: 'Males'}, {value: 'female', display: 'Females'}]

constructor(private memberService: MembersService){
  // accountService.currentUser$.pipe(take(1)).subscribe(user => {
  //   this.user = user;
  //   this.userParams = new UserParams(user);
  // });
  this.userParams = memberService.getUserParams();
}

  ngOnInit(): void {
// this.members$ = this.memberService.getMembers();

this.loadMembers();
  }

loadMembers() 
{
  this.memberService.setUserParams(this.userParams);
  this.memberService.getMembers(this.userParams).subscribe(response => {
    this.members = response.result;
    this.pagination = response.pagination;
  });
}

// loadMembers() {
//   this.memberService.getMembers().subscribe(members => {
//     this.members = members;
//   });
// }

resetFilters(){
  this.userParams = this.memberService.resetUserParams();
  this.loadMembers();
}

pageChanged(event: any) {
  this.userParams.pageNumber = event.page;
  this.memberService.setUserParams(this.userParams);
  this.loadMembers();
}
}
