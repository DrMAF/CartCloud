import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginResponse, LoginRequest } from '../models';
import { environment } from '../../environments/environment.development';
import { JwtHelperService } from '@auth0/angular-jwt';
import { PaginatedResult, User } from '../models/user.model';

const baseUrl = environment.APIUrl + "/users";

@Injectable({
  providedIn: 'root'
})

export class UsersService {
  isLoggedIn = false;
  constructor(private http: HttpClient,) { }

  login(credintials: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${environment.APIUrl}/Users/Login`, credintials)
  }

  logout() {
    this.isLoggedIn = false;
    //this.setToken("");
    localStorage.removeItem("authToken");
  }

  setToken(token: string) {
    localStorage.setItem("authToken", token);
    this.isLoggedIn = true;
  }

  //getToken(): string | null {
  //  let token = localStorage.getItem("authToken");

  //  if (token != null) {
  //    token = JSON.parse(token);
  //  }

  //  return token;
  //}

  //isAuthenticated(): boolean {
  //  return this.isLoggedIn;
  //}

  register(registerModel: LoginRequest): Observable<any> {
    return this.http.post<any>(`${environment.APIUrl}/Users/RegisterUser`, registerModel)
  }

  public isAuthenticated(): boolean {
    let token = localStorage.getItem("authToken");

    //let token = localStorage.getItem("authToken");

    //if (token != null) {
    //  token = JSON.parse(token);
    //}

    const helper = new JwtHelperService();
    this.isLoggedIn = !helper.isTokenExpired(token);
    let kk = helper.decodeToken();

    console.log("kk: ", kk);

    return this.isLoggedIn;
  }



  getAll(search: string = ""): Observable<PaginatedResult<User>> {
    return this.http.get<PaginatedResult<User>>(baseUrl + "?search=" + search);
  }

  getById(id: any): Observable<User> {
    return this.http.get<User>(`${baseUrl}/getById?userId=${id}`);
  }

  create(data: User): Observable<any> {
    return this.http.post(baseUrl, data);
  }

  update(id: any, data: User): Observable<any> {
    return this.http.put(`${baseUrl}`, data);
  }

  delete(id: any): Observable<any> {
    return this.http.delete(`${baseUrl}?userId=${id}`);
  }
}
