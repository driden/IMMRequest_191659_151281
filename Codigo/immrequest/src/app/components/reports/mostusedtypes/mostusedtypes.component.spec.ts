import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MostusedtypesComponent } from './mostusedtypes.component';

describe('MostusedtypesComponent', () => {
  let component: MostusedtypesComponent;
  let fixture: ComponentFixture<MostusedtypesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MostusedtypesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MostusedtypesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
