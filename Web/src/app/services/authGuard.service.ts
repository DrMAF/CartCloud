import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { UsersService } from "../services/users.service";

@Injectable({
  providedIn: 'root'
})

export class AuthGuard {
  constructor(private userService: UsersService, private router: Router) { }

  canActivate(): boolean {
    if (this.userService.isAuthenticated()) {
      return true;
    }
    else {
      this.router.navigate(['/Login']);
      return false;
    }
  }
}
