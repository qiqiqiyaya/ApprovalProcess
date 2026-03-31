import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParallelApprovalMergeComponent } from './parallel-approval-merge.component';

describe('ParallelApprovalMergeComponent', () => {
  let component: ParallelApprovalMergeComponent;
  let fixture: ComponentFixture<ParallelApprovalMergeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ParallelApprovalMergeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ParallelApprovalMergeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
