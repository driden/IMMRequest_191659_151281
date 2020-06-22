import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { RequestsService } from 'src/app/services/requests.service';
import { Subscription } from 'rxjs';

import { HttpErrorResponse } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { FullRequest } from 'src/app/models/FullRequest';
import { AuthService } from 'src/app/services/auth.service';
import { User } from 'src/app/models/User';

@Component({
  selector: 'app-view-request',
  templateUrl: './view-request.component.html',
  styleUrls: ['./view-request.component.css'],
})
export class ViewRequestComponent implements OnInit, OnDestroy {
  reqSub: Subscription;
  updateSub: Subscription;
  loginSub: Subscription;

  constructor(
    private route: ActivatedRoute,
    private requestService: RequestsService,
    private authService: AuthService
  ) {}

  user: User = null;
  errorMsg = '';
  request: FullRequest = null;
  stateInSpanish = '';
  stateClass = '';
  availableStates = [];

  ngOnInit(): void {
    this.route.params.subscribe((reqId: Params) => {
      const id = +reqId.id;
      this.reqSub = this.requestService
        .get(id)
        .pipe(
          tap(
            (next) => {},
            (e) => this.setError(e)
          )
        )
        .subscribe((req: FullRequest) => {
          this.request = req;
          console.log(this.request);
          this.updateAvailableStates();
        });
    });
    this.loginSub = this.authService
      .login('admin@foo.com', 'pass')
      .pipe(tap((next) => {}, this.setError))
      .subscribe(console.log);
    this.authService.userSubject.subscribe((user: User) => (this.user = user));
  }

  ngOnDestroy(): void {
    this.reqSub && this.reqSub.unsubscribe();
    this.updateSub && this.updateSub.unsubscribe();
  }

  setError = (error: HttpErrorResponse) => {
    console.log(error);
    this.errorMsg =
      error.error.error || error.error.title || 'An error occurred!';
  };

  translateState = (state: string): string =>
    this.requestService.getStateInSpanish(state);

  getAvailableStates(): string[] {
    return this.requestService.getAvailableStates(this.request.requestState);
  }

  updateRequestState(newState: string) {
    this.updateSub = this.requestService
      .update(this.request.requestId, newState)
      .pipe(
        tap(
          (next) => {
            this.stateInSpanish = this.translateState(newState);
            this.request.requestState = newState;
          },
          (e) => this.setError(e)
        )
      )
      .subscribe();
  }
  updateAvailableStates = () => {
    this.stateInSpanish = this.translateState(this.request.requestState);
    this.availableStates = this.requestService
      .getAvailableStates(this.request.requestState)
      .map(this.translateState);
  };

  getClassForState(): string {
    switch (this.request.requestState) {
      case 'Created':
        return 'badge-secondary';
      case 'InReview':
        return 'badge-warning';
      case 'Accepted':
        return 'badge-success';
      case 'Denied':
        return 'badge-danger';
      case 'Done':
        return 'badge-dark';
      default:
        return '';
    }
  }
}
