import { Component, OnInit } from '@angular/core';

import {AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-new-request',
  templateUrl: './new-request.component.html',
  styleUrls: ['./new-request.component.css']
})
export class NewRequestComponent implements OnInit {

  constructor(private authService:AuthService) { }

  ngOnInit(): void {
    this.authService.login('admin@foo.com','pass').subscribe(res => {
      console.log(res);
      console.log(this.authService.userSubject.value)
    })
  }


}
