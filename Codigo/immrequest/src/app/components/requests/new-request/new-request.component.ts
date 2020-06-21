import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import {
  NgForm,
  FormControl,
  FormGroup,
  FormArray,
  Validators,
} from '@angular/forms';
import { Subscription } from 'rxjs';

import { AuthService } from '../../../services/auth.service';
import { AreasService } from 'src/app/services/areas.service';
import { Area } from 'src/app/models/Area';
import { Topic } from 'src/app/models/Topic';
import { Type } from 'src/app/models/Type';
import { TopicsService } from 'src/app/services/topics.service';
import { TypesService } from 'src/app/services/types.service';
import { AdditionalField } from '../../../models/AdditionalField';

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

  areas: Area[] = [];
  topics: Topic[] = [];
  types: Type[] = [];
  additionalFields: AdditionalField[] = [];

  newRequestForm: FormGroup;

  constructor(
    private authService: AuthService,
    private areasService: AreasService,
    private topicService: TopicsService,
    private typeService: TypesService
  ) {}

  ngOnDestroy(): void {
    this.loginSub.unsubscribe();
    this.areasSub.unsubscribe();
    this.topicSub.unsubscribe();
    this.typeSub.unsubscribe();
  }

  ngOnInit(): void {
    this.authService.login('admin@foo.com', 'pass').subscribe(console.log);
    this.areasService.getAll().subscribe((areas: Area[]) => {
      this.areas = areas;
    });
    this.initForm();
  }

  onAreaSelected(areaId: number) {
    this.topicService
      .getAllInArea(areaId)
      .subscribe((topics: Topic[]) => (this.topics = topics));
  }

  onTopicSelected(topicId: number) {
    this.typeService
      .getAllInTopic(topicId)
      .subscribe((types: Type[]) => (this.types = types));
  }

  onTypeSelected(typeId: number) {
    this.newRequestForm.get('typeId').setValue(typeId);
    this.additionalFields = this.types.filter(
      (t: Type) => t.id !== typeId
    )[0].additionalFields;
    console.log(`additional fields`, this.additionalFields)
  }

  onSubmit() {
    console.log('submitted');
  }

  loadAdditionalFields(): void {
    for (const additionalField of this.additionalFields) {
      (this.newRequestForm.get('additionalFields') as FormArray).push(
        new FormGroup({
          name: new FormControl(additionalField.name, Validators.required),
          type: new FormControl(additionalField.fieldType),
          value: new FormControl('', Validators.required),
        })
      );
    }
  }

  getAdditionalFieldInputType(fieldType: string): string {
    switch (fieldType) {
      case 'integer':
        return 'number';
        break;
      case 'boolean':
        return 'checkbox';
        break;
      default:
        return fieldType;
        break;
    }
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
      additionalFields: new FormArray([]),
      // areaId: new FormControl(null, Validators.required),
      // topicId: new FormControl(null, Validators.required),
      typeId: new FormControl(typeId, [
        Validators.required,
        Validators.pattern(/[1-9]+[0-9]*/),
      ]),
    });
  }
}
