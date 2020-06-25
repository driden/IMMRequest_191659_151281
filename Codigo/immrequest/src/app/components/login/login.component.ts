import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { Subscription } from 'rxjs';

import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit, OnDestroy {
  loginSub: Subscription = null;
  errorMsg = '';
  user = { email: 'admin@foo.com', password: 'pass' };

  constructor(private router: Router, private authService: AuthService) {}

  ngOnInit(): void {}

  ngOnDestroy(): void {
    this.loginSub && this.loginSub.unsubscribe();
  }

  onSubmit(): void {
    this.loginSub = this.authService
      .login(this.user.email, this.user.password)
      .pipe(
        tap(() => {
          this.router.navigate(['/dashboard']);
        }, this.handleError)
      )
      .subscribe();
  }

  handleError = (error) => {
    this.errorMsg =
      error || error.error.error || error.error.title || 'An error occurred!';
  };
}
