import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TypesService {
  constructor(private http: HttpClient) {}

  get(topicId: number): Observable<Type[]> {
    return this.http.get()
  }
}

export interface Type {
  id: number;
  topicId: number;
  name: string;
  isActive: boolean;
  additionalFields: AdditionalField[];
}

export interface AdditionalField {
  id: number;
  name: string;
  fieldType: string;
  isRequired: boolean;
  range: string[];
}
// {
//   "id": 1,
//   "topicId": 1,
//   "name": "Taxi - Acoso",
//   "additionalFields": [
//       {
//           "id": 1,
//           "name": "Matricula",
//           "fieldType": "text",
//           "isRequired": false,
//           "value": null,
//           "range": []
//       },
//       {
//           "id": 2,
//           "name": "Fecha y hora",
//           "fieldType": "date",
//           "isRequired": false,
//           "value": null,
//           "range": [
//               "20-06-2010",
//               "20-06-2030"
//           ]
//       },
//       {
//           "id": 3,
//           "name": "Nro de Movil",
//           "fieldType": "integer",
//           "isRequired": true,
//           "value": null,
//           "range": [
//               "0",
//               "99999999"
//           ]
//       }
//   ],
//   "isActive": true
// }
