import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParallelApprovalComponent } from './parallel-approval.component';

describe('ParallelApprovalComponent', () => {
  let component: ParallelApprovalComponent;
  let fixture: ComponentFixture<ParallelApprovalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ParallelApprovalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ParallelApprovalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
