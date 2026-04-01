import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrComponent } from './or.component';

describe('OrComponent', () => {
  let component: OrComponent;
  let fixture: ComponentFixture<OrComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OrComponent]
    });
    fixture = TestBed.createComponent(OrComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});