import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NewRequestComponent } from './components/requests/new-request/new-request.component';
import { LoginComponent } from './components/login/login.component';
import { AllrequestComponent } from './components/requests/allrequest/allrequest.component';
import { ReportAComponent } from './components/reports/report-a/report-a.component';
import { ReportBComponent } from './components/reports/report-b/report-b.component';
const routes: Routes = [
  {
    path: 'dashboard',
    pathMatch: 'full',
    component: DashboardComponent,
  },
  {
    path: 'new-request',
    pathMatch: 'full',
    component: NewRequestComponent,
  },
  {
    path: 'login',
    pathMatch: 'full',
    component: LoginComponent,
  },
  {
    path: 'allrequest',
    pathMatch: 'full',
    component: AllrequestComponent,
  },
  {
    path: 'report-a',
    pathMatch: 'full',
    component: ReportAComponent,
  },
  {
    path: 'report-b',
    pathMatch: 'full',
    component: ReportBComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
