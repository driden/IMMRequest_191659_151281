import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { R } from 'src/app/services/requests.service';

@Component({
  selector: 'app-request-card',
  templateUrl: './request-card.component.html',
  styleUrls: ['./request-card.component.css'],
})
export class RequestCardComponent implements OnInit {
  @Input() request: R;

  constructor(private router:Router) {}

  ngOnInit(): void {
    console.log(this.request);
  }

  visit(reqId: number) {
    this.router.navigate(['view-request', reqId]);
  }

  getClassForState(): string {
    switch (this.request.status) {
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
