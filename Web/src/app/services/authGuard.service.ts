import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { UsersService } from "../services/users.service";

@Injectable({
  providedIn: 'root'
})

export class AuthGuard {
  constructor(private authService: UsersService, private router: Router) { }

  canActivate(): boolean {
    if (this.authService.isAuthenticated()) {
      return true;
    }
    else {
      this.router.navigate(['/Login']);
      return false;
    }
  }
}
