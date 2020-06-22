import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NewRequestComponent } from './components/requests/new-request/new-request.component';
import { ViewRequestComponent} from './components/requests/view-request/view-request.component';

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
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
