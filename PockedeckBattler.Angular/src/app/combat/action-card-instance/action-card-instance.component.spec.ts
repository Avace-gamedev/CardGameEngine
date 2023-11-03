import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActionCardInstanceComponent } from './action-card-instance.component';

describe('ActionCardInstanceComponent', () => {
  let component: ActionCardInstanceComponent;
  let fixture: ComponentFixture<ActionCardInstanceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ActionCardInstanceComponent]
    });
    fixture = TestBed.createComponent(ActionCardInstanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
