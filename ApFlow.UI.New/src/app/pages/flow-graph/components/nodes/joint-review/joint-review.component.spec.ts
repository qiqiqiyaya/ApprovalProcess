import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JointReviewComponent } from './joint-review.component';

describe('JointReviewComponent', () => {
  let component: JointReviewComponent;
  let fixture: ComponentFixture<JointReviewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [JointReviewComponent]
    });
    fixture = TestBed.createComponent(JointReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});