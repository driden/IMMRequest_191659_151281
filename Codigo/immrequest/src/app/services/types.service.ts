import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Type } from '../models/Type';
import { environment } from '../../environments/environment.prod';

@Injectable({
  providedIn: 'root',
})
export class TypesService {
  constructor(private http: HttpClient) {}

  getAllInTopic(topicId: number): Observable<Type[]> {
    return this.http.get<Type[]>(`${environment.serverUrl}/api/types/${topicId}`);
  }
}
