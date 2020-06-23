import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NewRequestComponent } from './components/requests/new-request/new-request.component';
import { ViewRequestComponent} from './components/requests/view-request/view-request.component';
import { LoginComponent } from './components/login/login.component';
import { NewTypeComponent } from './components/types/new-type/new-type.component';

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
    path: 'view-request/:id',
    pathMatch: 'full',
    component: ViewRequestComponent,
  },
  {
    path: 'new-type',
    pathMatch: 'full',
    component: NewTypeComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
