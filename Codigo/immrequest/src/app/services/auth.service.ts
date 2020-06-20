import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, BehaviorSubject, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { environment } from '../../environments/environment.prod';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  userSubject = new BehaviorSubject<User>(null);
  constructor(private http: HttpClient) {}

  login(email: string, password: string): Observable<AuthResponse> {
    return this.http
      .post<AuthResponse>(environment.serverUrl + '/api/sessions', {
        email,
        password,
      })
      .pipe(
        catchError(this.handleError),
        tap((resData: AuthResponse) => {
          this.handleAuthentication(email, resData.token);
        })
      );
  }

  private handleError(errorResponse: HttpErrorResponse): Observable<never> {
    let errorMsg = 'An unknown error occurred';
    if (!errorResponse.error || !errorResponse.error.error) {
      return throwError(errorMsg);
    }

    return throwError(errorMsg);
  }

  private handleAuthentication(email: string, token: string): void {
    const user = new User(email, token);
    this.userSubject.next(user);
  }
}

export interface AuthRequest {
  email: string;
  password: string;
}

export interface AuthResponse {
  token: string;
}
