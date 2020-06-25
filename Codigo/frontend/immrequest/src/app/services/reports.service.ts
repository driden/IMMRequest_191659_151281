import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment.prod';

@Injectable({providedIn: 'root'})
export class ReportsService
{
  constructor(private http: HttpClient){

  }

  getDataReportByMail(data: any)
  {
    return this.http.get(`${environment.serverUrl}/api/reports/a/`,
    { params: { mail: data.mail, startDate: data.startDate, endDate: data.endDate }});
  }

  getDataReportMostUsedTypes(data: any)
  {

    return this.http.get(`${environment.serverUrl}/api/reports/b/`, {
      params: { startDate: data.startDate, endDate: data.endDate }});
  }


}
