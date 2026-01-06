/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ParallelApprovalMergeNodeComponent } from './parallel-approval-Merge-node.component';

describe('ParallelApprovalMergeNodeComponent', () => {
  let component: ParallelApprovalMergeNodeComponent;
  let fixture: ComponentFixture<ParallelApprovalMergeNodeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ParallelApprovalMergeNodeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ParallelApprovalMergeNodeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
