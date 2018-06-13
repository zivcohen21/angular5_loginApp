import { Injectable, Inject } from '@angular/core';
import { Response } from '@angular/http';
import { HttpClient, } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
@Injectable()
export class UserService {
  myAppUrl: string = "";
  constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl;
  }
  getUsers() {
    return this._http.get(this.myAppUrl + 'api/User/Index')
      .map((response: Response) => response.json())
      .catch(this.errorHandler);
  }
  getUserById(id: number) {
    return this._http.get(this.myAppUrl + "api/User/Details/" + id)
      .map((response: Response) => response.json())
      .catch(this.errorHandler)
  }
  saveUser(user) {
    return this._http.post(this.myAppUrl + 'api/User/Create', user)
      .map((response: Response) => response)
      .catch(this.errorHandler)
  }
  updateUser(user) {
    return this._http.put(this.myAppUrl + 'api/User/Edit', user)
      .map((response: Response) => response.json())
      .catch(this.errorHandler);
  }
  deleteUser(id: number) {
    return this._http.delete(this.myAppUrl + "api/User/Delete/" + id)
      .map((response: Response) => response.json())
      .catch(this.errorHandler);
  }
  errorHandler(error: Response) {
    console.log(error);
    return Observable.throw(error);
  }
}
