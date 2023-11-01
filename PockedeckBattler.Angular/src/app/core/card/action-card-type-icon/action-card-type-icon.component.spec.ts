import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActionCardTypeIconComponent } from './action-card-type-icon.component';

describe('CardTypeIconComponent', () => {
  let component: ActionCardTypeIconComponent;
  let fixture: ComponentFixture<ActionCardTypeIconComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ActionCardTypeIconComponent],
    });
    fixture = TestBed.createComponent(ActionCardTypeIconComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
