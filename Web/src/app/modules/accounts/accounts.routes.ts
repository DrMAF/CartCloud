import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { NgModule } from '@angular/core';
import { RegisterComponent } from './register/register.component';

export const routes: Routes = [
  { path: "", redirectTo: "login", pathMatch: "full" },
  { path: "login", component: LoginComponent },
  { path: "register", component: RegisterComponent },

  //{ path: 'Customers', canActivate: [AuthGuard], component: HomeComponent, loadChildren: () => import('./features/bar-cloud-ng/bar-cloud-ng.module').then(m => m.BarCloudNgModule) }
  //{ path: 'account', loadChildren: () => import(`./account/accounts.module`).then(r => r.AccountsModule) },

];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class AccountsRoutesModule {
}
