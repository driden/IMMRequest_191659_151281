import { Component, OnInit } from '@angular/core';
import { ReportsService } from 'src/app/services/reports.service';
import { take } from 'rxjs/operators';
import { FormBuilder } from '@angular/forms';

export interface ReportAData {
  stateName: string;
  quantity: number;
  ids: number[];
}

@Component({
  selector: 'app-searchByMail',
  templateUrl: './searchByMail.component.html',
  styleUrls: ['./searchByMail.component.css']
})


export class SearchByMailComponent implements OnInit {

  reportData: ReportAData[] = [];
  checkoutForm;

  constructor(
    private reportsService: ReportsService,

    private formBuilder: FormBuilder,
    ) {
      this.checkoutForm = this.formBuilder.group({
        mail: '',
        startDate: '',
        endDate: '',
      });
    }

  ngOnInit(): void {

  }

  proccedDataReport(mail: string, startDate: Date, endDate: Date): void {
    this.reportsService.getDataReportByMail({
      "mail": mail,
	    "startDate": startDate,
      "endDate": endDate,
    })
    .pipe(take(1))
    .subscribe(
      (res: any[]) => { console.log(res)
          this.reportData = res;
      },
      (error) => { console.log(error)},
    )
  }

  onSubmit(searchData) {

    this.proccedDataReport(searchData.mail, searchData.startDate, searchData.endDate);

    console.log('Search', searchData);
  }
}
