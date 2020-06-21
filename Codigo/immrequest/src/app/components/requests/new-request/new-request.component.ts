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
  }

  onSubmit() {
    console.log('submitted');
  }

  private initForm(): void {
    let citizenName = '';
    let citizenEmail = '';
    let reqDescription = '';
    let typeId = -1;

    const reqAdditionalFields = new FormArray([]);
    // for (const ingredient of recipe.ingredients) {
    //   reqAdditionalFields.push(
    //     new FormGroup({
    //       name: new FormControl(ingredient.name, Validators.required),
    //       amount: new FormControl(ingredient.amount, [
    //         Validators.required,
    //         Validators.pattern(/^[1-9]+[0-9]*$/),
    //       ]),
    //     })
    //   );
    // }

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
      additionalFields: reqAdditionalFields,
      // areaId: new FormControl(null, Validators.required),
      // topicId: new FormControl(null, Validators.required),
      typeId: new FormControl(typeId, [
        Validators.required,
        Validators.pattern(/[1-9]+[0-9]*/),
      ]),
    });
  }
}
