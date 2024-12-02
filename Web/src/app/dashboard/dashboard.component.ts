import { Component } from '@angular/core';
import { HeaderComponent } from './header/header.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { FooterComponent } from './footer/footer.component';

import { CommonModule } from '@angular/common';
import { BodyComponent } from '../body/body.component';


interface SideNavToggle {
  screenWidth: number;
  collapsed: boolean;
}


@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [HeaderComponent, SidebarComponent, FooterComponent, CommonModule, BodyComponent],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})

export class DashboardComponent {
  dashoboarshow= false;
  isSideNavCollapsed = false;
  screenWidth = 0;

  onToggleSideNav(data: SideNavToggle): void {
    this.screenWidth = data.screenWidth;
    this.isSideNavCollapsed = data.collapsed;
    this.dashoboarshow= false
  }
}
