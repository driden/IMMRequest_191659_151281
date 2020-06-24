import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NewRequestComponent } from './components/requests/new-request/new-request.component';
import { ViewRequestComponent } from './components/requests/view-request/view-request.component';
import { LoginComponent } from './components/login/login.component';
import { AllrequestComponent } from './components/requests/allrequest/allrequest.component';
import { SearchByMailComponent } from './components/reports/searchByMail/searchByMail.component';
import { MostusedtypesComponent } from './components/reports/mostusedtypes/mostusedtypes.component';
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
    path: 'searchByMail',
    pathMatch: 'full',
    component: SearchByMailComponent,
  },
  {
    path: 'mostusedtypes',
    pathMatch: 'full',
    component: MostusedtypesComponent,
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
