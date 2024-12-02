import { NgIf } from '@angular/common';
import { AfterViewInit, Component } from '@angular/core';
import { Router } from '@angular/router';
import { UsersService } from '../../../services/users.service';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { LoginRequest } from '../../../models';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [NgIf, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements AfterViewInit {
  inValidLogin = false;

  loginForm = new FormGroup({
    email: new FormControl('', Validators.compose([Validators.required, Validators.pattern("[A-Za-z0-9._%-]+@[A-Za-z0-9._%-]+\\.[a-z]{2,3}")])),
    password: new FormControl('', Validators.compose([Validators.required, Validators.pattern("\\S{8,32}")])),
    KeepIn: new FormControl(false)
  });
  constructor(private router: Router, private loginService: UsersService) {
  }

  ngAfterViewInit(): void {
  }

  login() {
    this.loginService.login(new LoginRequest(this.loginForm.value)).subscribe(e => {
      if (e.success) {
        this.loginService.setToken(e.token);
        this.router.navigate(["Customers"]);
      }
      else {
        this.inValidLogin = true;
      }
    });
  }
}
