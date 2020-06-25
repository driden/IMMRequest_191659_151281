import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { RequestsService, R } from 'src/app/services/requests.service';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-my-requests',
  templateUrl: './my-requests.component.html',
  styleUrls: ['./my-requests.component.css'],
})
export class MyRequestsComponent implements OnInit {
  @ViewChild('emailInput', { static: false }) emailInput: ElementRef;
  constructor(private requestService: RequestsService) {}

  ngOnInit(): void {}

  requests: R[] = [];
  errorMsg = '';

  buscar() {
    this.requestService
      .getAllByEmail(this.emailInput.nativeElement.value)
      .pipe(
        tap(
          (r: R[]) => {
            this.requests = r;
          },
          (e) => {
            this.errorMsg =
              e.error.error || e.error.title || e.error || 'Ocurrió un error';
          }
        )
      )
      .subscribe();
  }
}
