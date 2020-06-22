import { Component, OnInit, OnDestroy } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { tap } from 'rxjs/operators';

import { AuthService } from '../../../services/auth.service';
import { AreasService } from 'src/app/services/areas.service';
import { Area } from 'src/app/models/Area';
import { Topic } from 'src/app/models/Topic';
import { Type } from 'src/app/models/Type';
import { Request as NewRequest } from 'src/app/models/Request';
import { TopicsService } from 'src/app/services/topics.service';
import { TypesService } from 'src/app/services/types.service';
import { AdditionalField } from '../../../models/AdditionalField';
import { RequestsService } from 'src/app/services/requests.service';

@Component({
  selector: 'app-new-request',
  templateUrl: './new-request.component.html',
  styleUrls: ['./new-request.component.css'],
})
export class NewRequestComponent implements OnInit, OnDestroy {
  areasSub: Subscription;
  loginSub: Subscription;
  topicSub: Subscription;
  typeSub: Subscription;
  reqSub: Subscription;

  areas: Area[] = [];
  topics: Topic[] = [];
  types: Type[] = [];
  additionalFields: AdditionalField[] = [];

  newRequestForm: FormGroup;

  errorMsg = '';

  constructor(
    private router: Router,
    private authService: AuthService,
    private areasService: AreasService,
    private topicService: TopicsService,
    private typeService: TypesService,
    private requestService: RequestsService
  ) {}

  ngOnDestroy(): void {
    this.loginSub && this.loginSub.unsubscribe();
    this.areasSub && this.areasSub.unsubscribe();
    this.topicSub && this.topicSub.unsubscribe();
    this.typeSub && this.typeSub.unsubscribe();
    this.reqSub && this.reqSub.unsubscribe();
  }

  ngOnInit(): void {
    this.loginSub = this.authService
      .login('admin@foo.com', 'pass')
      .pipe(tap((next) => {}, this.setError))
      .subscribe(console.log);
    this.areasSub = this.areasService
      .getAll()
      .pipe(tap((next) => {}, this.setError))
      .subscribe((areas: Area[]) => {
        this.areas = areas;
        this.topics = [];
        this.types = [];
      });
    this.initForm();
  }

  onAreaSelected(areaId: number) {
    this.topicSub = this.topicSub = this.topicService
      .getAllInArea(areaId)
      .pipe(tap((next) => {}, this.setError))
      .subscribe((topics: Topic[]) => {
        this.topics = topics;
        this.types = [];
      });
  }

  onTopicSelected(topicId: number) {
    this.typeSub = this.typeService
      .getAllInTopic(topicId)
      .pipe(tap((next) => {}, this.setError))
      .subscribe((types: Type[]) => {
        this.types = types;
      });
  }

  onTypeSelected(typeId: number) {
    this.newRequestForm.get('typeId').setValue(typeId);
    this.additionalFields = this.types.filter(
      (t: Type) => t.id !== typeId
    )[0].additionalFields;
    this.loadAdditionalFields();
  }

  setError = (error: HttpErrorResponse) => {
    console.log(error);
    this.errorMsg = error.error.title || error.error || 'An error occurred!';
  };

  loadAdditionalFields(): void {
    let additionalFieldsForm = this.newRequestForm.get('afArray') as FormArray;

    additionalFieldsForm.clear();

    for (const additionalField of this.additionalFields) {
      const afFormGroup = new FormGroup({});
      afFormGroup.addControl(
        additionalField.name,
        new FormControl(
          null,
          additionalField.isRequired ? [Validators.required] : []
        )
      );
      additionalFieldsForm.push(afFormGroup);
    }
  }

  getAdditionalFieldInputType(fieldType: string): string {
    switch (fieldType) {
      case 'integer':
        return 'number';
      case 'boolean':
        return 'checkbox';
      default:
        return fieldType;
    }
  }

  onSubmit() {
    const req = this.createNewRequest();
    console.log('attempting request', req);
    this.reqSub = this.requestService
      .add(req)
      .pipe(tap(() => {}, this.setError))
      .subscribe((req: { id: string }) => {
        console.log(req.id);
        this.router.navigate(['/view-request', req.id]);
      });
  }

  private createNewRequest(): NewRequest {
    const req: NewRequest = {
      details: this.newRequestForm.get('details').value,
      email: this.newRequestForm.get('email').value,
      name: this.newRequestForm.get('name').value,
      phone: this.newRequestForm.get('phone').value,
      typeId: +this.newRequestForm.get('typeId').value,
      additionalFields: this.newRequestForm.get('afArray').value,
    };

    let newArray = [];
    for (let e of req.additionalFields) {
      const field = Object.keys(e)[0];
      if (!!e[field]) {
        newArray.push({ name: field, values: `${e[field]}` });
      }
    }
    req.additionalFields = newArray;
    return req;
  }
  private initForm(): void {
    let citizenName = '';
    let citizenEmail = '';
    let reqDescription = '';
    let typeId = -1;

    this.newRequestForm = new FormGroup({
      name: new FormControl(citizenName, Validators.required),
      email: new FormControl(citizenEmail, [
        Validators.required,
        Validators.email,
      ]),
      details: new FormControl(reqDescription, [
        Validators.required,
        Validators.maxLength(2000),
      ]),
      phone: new FormControl(null, [
        Validators.required,
        Validators.pattern(/^[+]?[(]?[0-9]+[)]?[-0-9]*[0-9]$/),
      ]),
      afArray: new FormArray([]),
      areaId: new FormControl(null, Validators.required),
      topicId: new FormControl(null, Validators.required),
      typeId: new FormControl(typeId, [
        Validators.required,
        Validators.pattern(/[1-9]+[0-9]*/),
      ]),
    });
  }
}
