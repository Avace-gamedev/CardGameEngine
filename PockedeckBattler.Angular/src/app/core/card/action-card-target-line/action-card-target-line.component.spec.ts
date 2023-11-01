import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActionCardTargetLineComponent } from './action-card-target-line.component';

describe('ActionCardTargetLineComponent', () => {
  let component: ActionCardTargetLineComponent;
  let fixture: ComponentFixture<ActionCardTargetLineComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ActionCardTargetLineComponent]
    });
    fixture = TestBed.createComponent(ActionCardTargetLineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
