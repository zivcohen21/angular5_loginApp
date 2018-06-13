import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../services/userservice.service';
import { AuthenticationService } from '../services/authentication.service';
import { Md5 } from 'ts-md5';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent implements OnInit {

  model: any = {};
  loading = false;

  constructor(private router: Router,
    private userService: UserService,
    private authenticationService: AuthenticationService
  ) { }

  ngOnInit() {
  }

  register() {
    this.loading = true;
    const saltRounds = 10;
    const userPassword = this.model.password;
    this.model.password = Md5.hashStr(userPassword);
    this.userService.saveUser(this.model)
      .subscribe(
      data => {
          //this.alertService.success('Registration successful', true);
          this.router.navigate(['/login']);
        },
        error => {
          //this.alertService.error(error);
          this.loading = false;
        });
  }
}
