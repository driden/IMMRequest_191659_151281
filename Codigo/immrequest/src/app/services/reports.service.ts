import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FullRequest } from '../models/FullRequest';
import { environment } from '../../environments/environment.prod';

@Injectable({providedIn: 'root'})
export class ReportsService
{
  constructor(private http: HttpClient){

  }

  getDataReportByMail(data: any)
  {

    return this.http.get(
      `${environment.serverUrl}/api/reports/a/mail=${data.mail}&startDate=${data.startDate}&endDate=${data.endDate}`);

  }

  getDataReportMostUsedTypes(data: any)
  {
    return this.http.get(
      `${environment.serverUrl}/api/reports/b/startDate=${data.startDate}&endDate=${data.endDate}`);

  }


}
