import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NewRequestComponent } from './components/requests/new-request/new-request.component';
import { ViewRequestComponent } from './components/requests/view-request/view-request.component';
import { LoginComponent } from './components/login/login.component';
import { AllrequestComponent } from './components/requests/allrequest/allrequest.component';
import { ReportAComponent } from './components/reports/report-a/report-a.component';
import { ReportBComponent } from './components/reports/report-b/report-b.component';
import { NewTypeComponent } from './components/types/new-type/new-type.component';
import { FileImportComponent } from './components/file-import/file-import.component';

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
    path: 'view-request/:id',
    pathMatch: 'full',
    component: ViewRequestComponent,
  },
  {
    path: 'login',
    pathMatch: 'full',
    component: LoginComponent,
  },
  {
    path: 'import',
    pathMatch: 'full',
    component: FileImportComponent,
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
  },
  {
    path: 'new-type',
    pathMatch: 'full',
    component: NewTypeComponent,
  },
  { path: '**', component: DashboardComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
