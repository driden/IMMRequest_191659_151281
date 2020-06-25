import { Injectable } from '@angular/core';
import { Admin } from '../models/Admin';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment.prod';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class AdminsService {
  constructor(private http: HttpClient) {}

  add(admin: Admin): Observable<{ id: number }> {
    return this.http.post<{ id: number }>(
      `${environment.serverUrl}/api/admins`, admin
    );
  }
}
