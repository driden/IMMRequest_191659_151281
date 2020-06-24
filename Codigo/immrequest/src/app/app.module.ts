import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NewRequestComponent } from './components/requests/new-request/new-request.component';
import { ViewRequestComponent } from './components/requests/view-request/view-request.component';
import { DropdownDirective } from './shared/dropdown.directive';
import { LoginComponent } from './components/login/login.component';
import { IndexComponent } from './components/index/index.component';
import { SearchByMailComponent } from './components/reports/searchByMail/searchByMail.component';
import { MostusedtypesComponent } from './components/reports/mostusedtypes/mostusedtypes.component';
import { AuthInterceptorService } from './services/auth-interceptor.service';
import { NewTypeComponent } from './components/types/new-type/new-type.component';
import { FileImportComponent } from './components/file-import/file-import.component';
import { ViewAllRequestsComponent } from './components/requests/view-all-requests/view-all-requests.component';
import { ErrorComponent } from './components/shared/error/error.component';
import { AddAdminComponent } from './components/add-admin/add-admin.component';
import { SuccessComponent } from './components/shared/success/success.component';

@NgModule({
  declarations: [
    AppComponent,
    SidebarComponent,
    DashboardComponent,
    NewRequestComponent,
    ViewRequestComponent,
    DropdownDirective,
    LoginComponent,
    IndexComponent,
    SearchByMailComponent,
    MostusedtypesComponent,
    NewTypeComponent,
    FileImportComponent,
    ViewAllRequestsComponent,
    ErrorComponent,
    AddAdminComponent,
    SuccessComponent,
  ],
  imports: [
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
