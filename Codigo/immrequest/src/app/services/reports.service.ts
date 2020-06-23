import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FullRequest } from '../models/FullRequest';
import { environment } from '../../environments/environment.prod';

@Injectable({providedIn: 'root'})
export class ReportsService
{
  constructor(private http: HttpClient){

  }

  getDataReportA(data: any)
  {

    return this.http.post(
      `${environment.serverUrl}/api/reports/a`,
      data
    );
  }
}
