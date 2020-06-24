import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment.prod';
import { Request } from '../models/Request';
import { Observable } from 'rxjs';
import { FullRequest } from '../models/FullRequest';
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

  get(id: number): Observable<FullRequest> {
    return this.http.get<FullRequest>(
      `${environment.serverUrl}/api/requests/${id}`
    );
  }

  getAll(): Observable<FullRequest[]> {
    return this.http.get<FullRequest[]>(
      `${environment.serverUrl}/api/requests`
    );
  }

  update(id: number, newState: string) {
    return this.http.put(`${environment.serverUrl}/api/requests/${id}`, {
      newState,
    });
  }

  getStateInSpanish(state: string): string {
    switch (state) {
      case 'Created':
        return 'Creada';
      case 'InReview':
        return 'En revisión';
      case 'Accepted':
        return 'Aceptada';
      case 'Denied':
        return 'Denegada';
      case 'Done':
        return 'Finalizada';
      default:
        return 'No MAPEADA';
    }
  }

  getAvailableStates(state: string): string[] {
    const moves = [
      {
        state: 'Created',
        to: ['InReview'],
      },
      {
        state: 'InReview',
        to: ['Created', 'Accepted', 'Denied'],
      },
      {
        state: 'Accepted',
        to: ['Done', 'InReview'],
      },
      {
        state: 'Denied',
        to: ['Done', 'InReview'],
      },
      {
        state: 'Done',
        to: ['Accepted', 'Denied'],
      },
    ];

    return moves.filter((move) => move.state === state)[0].to;
  }
}
