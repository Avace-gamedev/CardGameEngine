import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActionPointsComponent } from './action-points.component';

describe('ActionPointsComponent', () => {
  let component: ActionPointsComponent;
  let fixture: ComponentFixture<ActionPointsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ActionPointsComponent]
    });
    fixture = TestBed.createComponent(ActionPointsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
