import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { tap } from 'rxjs/operators';

import { AdminsService } from 'src/app/services/admins.service';
import { Admin } from 'src/app/models/Admin';

@Component({
  selector: 'app-add-admin',
  templateUrl: './add-admin.component.html',
  styleUrls: ['./add-admin.component.css'],
})
export class AddAdminComponent implements OnInit {
  errorMsg = '';
  successMsg = '';

  admin: Admin = { name: '', email: '', password: '', phoneNumber: '' };

  @ViewChild('adminForm') adminForm: NgForm;
  constructor(private adminsService: AdminsService) {}

  ngOnInit(): void {}

  onFormSubmit() {
    this.adminsService.add(this.admin).pipe(
      tap((r) => {
        this.successMsg =`Admin ${r.id} agregado` ;
      }, this.setError)
    ).subscribe();
  }

  setError = (error) => {
    this.errorMsg =
      error.error.error ||
      error.error.title ||
      error.error ||
      'Ocurrió un error';
  };
}
