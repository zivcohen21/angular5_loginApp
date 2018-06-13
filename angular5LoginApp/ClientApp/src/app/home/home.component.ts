import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../services/userservice.service'
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public userList: UserData[];
  p: number = 1;
  ipp: number = 10;
  constructor(public http: HttpClient, private _router: Router, private _userService: UserService, private authenticationService: AuthenticationService) {
    this.getUsers();
  }
  getUsers() {
    this._userService.getUsers().subscribe(
      data => this.userList = data
    )
  }
  //delete(userID) {
  //  var ans = confirm("Do you want to delete customer with Id: " + userID);
  //  if (ans) {
  //    this._userService.deleteUser(userID).subscribe((data) => {
  //      this.getUsers();
  //    }, error => console.error(error))
  //  }
  //}

  logout() {
    this.authenticationService.logout();
  }
}

interface UserData {
  UserID: number;
  FirstName: string;
  LastName: string;
  Username: string;
  Password: string;
}
