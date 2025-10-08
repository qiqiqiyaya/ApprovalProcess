/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { NodeOperationComponent } from './node-operation.component';

describe('NodeOperationComponent', () => {
  let component: NodeOperationComponent;
  let fixture: ComponentFixture<NodeOperationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NodeOperationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NodeOperationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
