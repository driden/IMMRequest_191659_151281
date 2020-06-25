import { Component, OnInit } from '@angular/core';
import { ReportsService } from 'src/app/services/reports.service';
import { take } from 'rxjs/operators';
import { FormBuilder } from '@angular/forms';

export interface ReportData {
  name: string;
  quantity: number;
}

@Component({
  selector: 'app-mostusedtypes',
  templateUrl: './mostusedtypes.component.html',
  styleUrls: ['./mostusedtypes.component.css'],
})
export class MostusedtypesComponent implements OnInit {
  reportData: ReportData[] = [];
  checkoutForm;
  errorMsg = '';

  constructor(
    private reportsService: ReportsService,

    private formBuilder: FormBuilder
  ) {
    this.checkoutForm = this.formBuilder.group({
      mail: '',
      startDate: '',
      endDate: '',
    });
  }

  ngOnInit(): void {}

  proccedDataReport(startDate: Date, endDate: Date): void {
    this.reportsService
      .getDataReportMostUsedTypes({
        startDate: startDate,
        endDate: endDate,
      })
      .pipe(take(1))
      .subscribe(
        (res: any[]) => {
          this.reportData = res;
          if (!res.length) {
            this.errorMsg = 'No hay resultados';
          }
        },
        (error) => {
          console.log(error);
        }
      );
  }

  onSubmit(searchData) {
    this.proccedDataReport(searchData.startDate, searchData.endDate);
  }
}
