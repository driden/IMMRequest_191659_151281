import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { NewRequestComponent } from './components/requests/new-request/new-request.component';
import { ViewRequestComponent } from './components/requests/view-request/view-request.component';
import { LoginComponent } from './components/login/login.component';
import { SearchByMailComponent } from './components/reports/searchByMail/searchByMail.component';
import { MostusedtypesComponent } from './components/reports/mostusedtypes/mostusedtypes.component';
import { NewTypeComponent } from './components/types/new-type/new-type.component';
import { FileImportComponent } from './components/file-import/file-import.component';
import { ViewAllRequestsComponent } from './components/requests/view-all-requests/view-all-requests.component';
import { AddAdminComponent } from './components/add-admin/add-admin.component';
import { TypesListComponent } from './components/types/types-list/types-list.component';
import { MyRequestsComponent } from './components/requests/my-requests/my-requests.component';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
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
    path: 'view-all-requests',
    pathMatch: 'full',
    component: ViewAllRequestsComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'view-my-requests',
    pathMatch: 'full',
    component: MyRequestsComponent,
  },
  {
    path: 'view-all-types',
    pathMatch: 'full',
    component: TypesListComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'search-by-mail',
    pathMatch: 'full',
    component: SearchByMailComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'most-used-types',
    pathMatch: 'full',
    component: MostusedtypesComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'new-type',
    pathMatch: 'full',
    component: NewTypeComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'add-admin',
    pathMatch: 'full',
    component: AddAdminComponent,
    canActivate: [AuthGuard],
  },
  { path: '**', redirectTo: '/view-my-requests' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
