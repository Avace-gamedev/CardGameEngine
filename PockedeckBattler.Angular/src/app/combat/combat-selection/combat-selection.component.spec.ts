import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CombatSelectionComponent } from './combat-selection.component';

describe('CombatSelectionComponent', () => {
  let component: CombatSelectionComponent;
  let fixture: ComponentFixture<CombatSelectionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CombatSelectionComponent]
    });
    fixture = TestBed.createComponent(CombatSelectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
