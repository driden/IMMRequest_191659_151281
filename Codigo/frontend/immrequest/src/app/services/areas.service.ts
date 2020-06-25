import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Area } from '../models/Area';
import { environment } from '../../environments/environment.prod';

@Injectable({
  providedIn: 'root',
})
export class AreasService {
  constructor(private http: HttpClient) {}

  getAll(): Observable<Area[]> {
    return this.http.get<Area[]>(environment.serverUrl + '/api/areas');
  }
}


