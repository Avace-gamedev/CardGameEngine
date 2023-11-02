import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CombatPreparationComponent } from './combat-preparation.component';

describe('CombatPreparationComponent', () => {
  let component: CombatPreparationComponent;
  let fixture: ComponentFixture<CombatPreparationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CombatPreparationComponent]
    });
    fixture = TestBed.createComponent(CombatPreparationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
