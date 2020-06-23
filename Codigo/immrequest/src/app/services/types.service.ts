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
    return this.http.get<Type[]>(
      `${environment.serverUrl}/api/types/${topicId}`
    );
  }

  add(type: Type) {
    return this.http
      .post<any>(`${environment.serverUrl}/api/types/`, type)
      .toPromise();
  }

  getAvailableTypes(): { value: string; text: string }[] {
    return [
      { value: 'date', text: 'fecha' },
      { value: 'text', text: 'texto' },
      { value: 'boolean', text: 'binario' },
      { value: 'int', text: 'entero' },
    ];
  }
}
