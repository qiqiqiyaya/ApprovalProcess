import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrMergeComponent } from './or-merge.component';

describe('OrMergeComponent', () => {
  let component: OrMergeComponent;
  let fixture: ComponentFixture<OrMergeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OrMergeComponent]
    });
    fixture = TestBed.createComponent(OrMergeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});