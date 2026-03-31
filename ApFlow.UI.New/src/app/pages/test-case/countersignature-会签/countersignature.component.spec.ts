/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { CountersignatureComponent } from './countersignature.component';

describe('CountersignatureComponent', () => {
  let component: CountersignatureComponent;
  let fixture: ComponentFixture<CountersignatureComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CountersignatureComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CountersignatureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
