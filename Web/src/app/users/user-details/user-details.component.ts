import { Component, Input } from '@angular/core';
import { User } from '../../models/user.model';
import { UsersService } from '../../services/users.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { KENDO_BUTTONS } from "@progress/kendo-angular-buttons";
import { LabelModule } from '@progress/kendo-angular-label';
import { KENDO_INPUTS } from '@progress/kendo-angular-inputs';

@Component({
  selector: 'app-user-details',
  standalone: true,
  imports: [FormsModule, RouterModule,
    LabelModule, KENDO_BUTTONS, KENDO_INPUTS],
  templateUrl: './user-details.component.html',
  styleUrl: './user-details.component.scss'
})

export class UserDetailsComponent {
  @Input() itemsCount = 0;

  @Input() currentUser: User = {
    id: 0,
    firstName: "",
    lastName: "",
    email: "",
    phoneNumber: ""
  };

  constructor(private userService: UsersService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
  }

  goUpdate() {
    this.router.navigate(["/update/" + this.currentUser.id]);
  }
}
