import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, OnDestroy } from '@angular/core';
import {
  FormGroup,
  FormArray,
  Validators,
  FormControl,
  AbstractControl,
} from '@angular/forms';
import { Subscription } from 'rxjs';
import { tap } from 'rxjs/operators';

import { AreasService } from 'src/app/services/areas.service';
import { TopicsService } from 'src/app/services/topics.service';
import { TypesService } from 'src/app/services/types.service';
import { Area } from 'src/app/models/Area';
import { Topic } from 'src/app/models/Topic';
import { Type } from 'src/app/models/Type';

@Component({
  selector: 'app-new-type',
  templateUrl: './new-type.component.html',
  styleUrls: ['./new-type.component.css'],
})
export class NewTypeComponent implements OnInit, OnDestroy {
  errorMsg = '';
  successMsg = '';
  uiTypes = [];
  areas: Area[] = [];
  topics: Topic[] = [];
  selectedTopic: Topic = null;
  areasSub: Subscription;
  topicSub: Subscription;
  typeSub: Subscription;
  newTypeForm: FormGroup;
  rangeValues: string[] = [];

  constructor(
    private areasService: AreasService,
    private topicService: TopicsService,
    private typeService: TypesService
  ) {}

  ngOnDestroy(): void {}

  ngOnInit(): void {
    this.areasSub = this.areasService
      .getAll()
      .pipe(tap((next) => {}, this.setError))
      .subscribe((areas: Area[]) => {
        this.areas = areas;
        this.topics = [];
      });
    this.initForm();
    this.uiTypes = this.typeService.getAvailableTypes();
  }

  onAreaSelected(areaId: number) {
    this.topicSub = this.topicSub = this.topicService
      .getAllInArea(areaId)
      .pipe(tap((next) => {}, this.setError))
      .subscribe((topics: Topic[]) => {
        this.topics = topics;
      });
  }

  onSubmit() {
    const type = this.createNewType();
    this.typeService
      .add(type)
      .then(() => this.successMsg = 'se agrego')
      .catch(this.setError);
  }

  private setError = (error: HttpErrorResponse) => {
    console.log(error);
    this.errorMsg = error.error.title || error.error || 'An error occurred!';
  };

  get additionalFieldsControls(): AbstractControl[] {
    return (this.newTypeForm.get('afArray') as FormArray).controls;
  }

  rangeControls(formGroupIndex: number): AbstractControl[] {
    return (this.newTypeForm
      .get('afArray')
      .get(`${formGroupIndex}`)
      .get('range') as FormArray).controls;
  }

  addMoreAdditionalFields() {
    (this.newTypeForm.get('afArray') as FormArray).push(
      new FormGroup({
        name: new FormControl(null, [Validators.required]),
        type: new FormControl(null, [Validators.required]),
        required: new FormControl(false),
        range: new FormArray([]),
      })
    );
  }

  addToRange(formGroupIndex: number) {
    let rangeArray: FormArray = this.newTypeForm
      .get('afArray')
      .get(`${formGroupIndex}`)
      .get('range') as FormArray;

    rangeArray.push(
      new FormGroup({
        rangeValue: new FormControl(null, Validators.required),
      })
    );
  }

  removeAdditionalFieldRangeEntry(indexField: number, indexAfRange: number) {
    let rangeArray: FormArray = this.newTypeForm
      .get('afArray')
      .get(`${indexField}`)
      .get('range') as FormArray;

    rangeArray.removeAt(indexAfRange);
  }

  deleteAdditionalField(afInde: number) {
    (this.newTypeForm.get('afArray') as FormArray).removeAt(afInde);
  }

  shouldDisableTypeDropdown(afIndex: number): boolean {
    let rangeArray: FormArray = this.newTypeForm
      .get('afArray')
      .get(`${afIndex}`)
      .get('range') as FormArray;
    return rangeArray.controls.length > 0;
  }

  private createNewType(): Type {
    const rangeArray: FormArray = this.newTypeForm.get('afArray') as FormArray;
    const additionalFields = rangeArray.value.reduce((fields, value) => {
      const { name, required, type, range } = value;
      const af = {
        name,
        fieldType: type,
        isRequired: required,
        range: range.map((r) => r.rangeValue),
      };

      fields.push(af);

      return fields;
    }, []);

    const type: Type = {
      id: -1,
      isActive: true,
      name: this.newTypeForm.get('name').value,
      topicId: +this.newTypeForm.get('topicId').value,
      additionalFields,
    };
    return type;
  }

  private initForm(): void {
    this.newTypeForm = new FormGroup({
      name: new FormControl('', Validators.required),
      afArray: new FormArray([]),
      areaId: new FormControl(null, Validators.required),
      topicId: new FormControl(null, Validators.required),
    });
  }
}
