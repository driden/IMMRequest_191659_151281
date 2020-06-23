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
import { AuthInterceptorService } from './services/auth-interceptor.service';
import { NewTypeComponent } from './components/types/new-type/new-type.component';

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
    NewTypeComponent,
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
