import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActionCardChipComponent } from './action-card-chip.component';

describe('ActionCardChipComponent', () => {
  let component: ActionCardChipComponent;
  let fixture: ComponentFixture<ActionCardChipComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ActionCardChipComponent]
    });
    fixture = TestBed.createComponent(ActionCardChipComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
