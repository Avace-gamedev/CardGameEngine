import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActionCardComponent } from './action-card.component';

describe('ActionCardComponent', () => {
  let component: ActionCardComponent;
  let fixture: ComponentFixture<ActionCardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ActionCardComponent]
    });
    fixture = TestBed.createComponent(ActionCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
