import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment.prod';
import { Request } from '../models/Request';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class RequestsService {
  constructor(private http: HttpClient) {}

  add(newReq: Request): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(
      `${environment.serverUrl}/api/requests`,
      newReq
    );
  }
}
