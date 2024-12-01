import { Routes } from '@angular/router';
import { UsersListComponent } from './users/users-list/users-list.component';
import { CreateUserComponent } from './users/create-user/create-user.component';
import { UserDetailsComponent } from './users/user-details/user-details.component';

export const routes: Routes = [
  { path: "", redirectTo: "users", pathMatch: "full" },
  { path: "users", component: UsersListComponent },
  { path: 'users/:id', component: UserDetailsComponent },
  { path: "create", component: CreateUserComponent },
  { path: "update/:id", component: CreateUserComponent },
  { path: "accounts", loadChildren: () => import(`./modules/accounts/accounts.module`).then(r => r.AccountsModule)  }

    //{ path: 'Customers', canActivate: [AuthGuard], component: HomeComponent, loadChildren: () => import('./features/bar-cloud-ng/bar-cloud-ng.module').then(m => m.BarCloudNgModule) }
    //{ path: 'account', loadChildren: () => import(`./account/accounts.module`).then(r => r.AccountsModule) },


];
