import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../../services/auth.service';
import { User } from 'src/app/models/User';
import { catchError, tap } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginSub: Subscription;

  user: FormGroup;

  errorMsg = '';

  constructor(
    private router: Router,
    private authService: AuthService,
  ) {}

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.loginSub.unsubscribe();
  }

  onSubmit(): void {
    console.log('login');
    var mail = this.user.get('mail').value;
    var password = this.user.get('password').value;

    this.loginSub = this.authService
      .login(mail, password)
      .pipe(tap((next) => {}, this.handleError))
      .subscribe(console.log);
      this.router.navigate(['/dashboard']);
  }

  handleError(error: HttpErrorResponse) {
    if (!error.error || !error.error.error) {
      this.errorMsg = 'An error ocurred!';
    }

    this.errorMsg = error.error.error;
  }

}
