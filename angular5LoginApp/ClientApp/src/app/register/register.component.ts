import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../services/userservice.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent implements OnInit {

  model: any = {};
  loading = false;

  constructor(private router: Router,
    private userService: UserService
  ) { }

  ngOnInit() {
  }

  register() {
    this.loading = true;

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
