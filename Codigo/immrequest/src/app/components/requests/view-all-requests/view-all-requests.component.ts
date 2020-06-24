import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { tap } from 'rxjs/operators';

import { FullRequest } from 'src/app/models/FullRequest';
import { RequestsService } from 'src/app/services/requests.service';

@Component({
  selector: 'app-view-all-requests',
  templateUrl: './view-all-requests.component.html',
  styleUrls: ['./view-all-requests.component.css'],
})
export class ViewAllRequestsComponent implements OnInit, OnDestroy {
  allRequests = [];
  errorMessage = '';
  requestSubscription: Subscription = null;
  constructor(
    private router: Router,
    private requestService: RequestsService
  ) {}

  ngOnDestroy(): void {
    this.requestSubscription && this.requestSubscription.unsubscribe();
  }

  ngOnInit(): void {
    this.requestSubscription = this.requestService
      .getAll()
      .pipe(
        tap(
          (requests: FullRequest[]) => {
            this.allRequests = requests;
            this.errorMessage = '';
          },
          (error: HttpErrorResponse) => {
            this.errorMessage =
              error.error.error || error.error.title || 'Ocurrió un error';
          }
        )
      )
      .subscribe();
  }

  visit(reqId: number) {
    this.router.navigate(['view-request', reqId]);
  }
}
