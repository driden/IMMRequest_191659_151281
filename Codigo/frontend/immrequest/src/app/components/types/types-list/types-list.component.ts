import { Component, OnInit } from '@angular/core';
import { AreasService } from 'src/app/services/areas.service';
import { TopicsService } from 'src/app/services/topics.service';
import { TypesService } from 'src/app/services/types.service';
import { tap } from 'rxjs/operators';

import { Area } from 'src/app/models/Area';
import { Type } from 'src/app/models/Type';
import { Topic } from 'src/app/models/Topic';

@Component({
  selector: 'app-types-list',
  templateUrl: './types-list.component.html',
  styleUrls: ['./types-list.component.css'],
})
export class TypesListComponent implements OnInit {
  constructor(
    private areasService: AreasService,
    private topicsService: TopicsService,
    private typesService: TypesService
  ) {}

  data = [];
  areas = [];
  topics = [];
  errorMsg = '';
  successMsg = '';

  ngOnInit(): void {
    this.areasService
      .getAll()
      .pipe(
        tap((areas: Area[]) => {
          this.areas = areas;
        }, this.setError)
      )
      .subscribe();
  }

  onAreaSelected(areaId: number) {
    this.topicsService
      .getAllInArea(areaId)
      .pipe(
        tap((topics: Topic[]) => {
          this.data = [];
          this.topics = topics;
        }, this.setError)
      )
      .subscribe();
  }

  onTopicSelected(topicId: number) {
    this.typesService
      .getAllInTopic(topicId)
      .pipe(tap((next) => {}, this.setError))
      .subscribe((types: Type[]) => {
        this.data = types;
      });
  }

  removeType(typeId: number) {
    this.typesService
      .delete(typeId)
      .pipe(
        tap(() => {
          this.successMsg = `se eliminó el tipo con id ${typeId}`;
          this.errorMsg = '';
          this.data = this.data.filter(topic => topic.id !== typeId);
        }, this.setError)
      )
      .subscribe();
  }

  setError = (error) => {
    this.errorMsg =
      error.error.error ||
      error.error.title ||
      error.error ||
      'Ocurrió un error';
  };
}
