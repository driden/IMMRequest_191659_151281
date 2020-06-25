import { Injectable } from '@angular/core';
import { Topic } from '../models/Topic';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class TopicsService {

  constructor(private http: HttpClient) {}

  getAllInArea(areaId: number): Observable<Topic[]> {
    return this.http.get<Topic[]>(`${environment.serverUrl}/api/topics/${areaId}`);
  }
}
