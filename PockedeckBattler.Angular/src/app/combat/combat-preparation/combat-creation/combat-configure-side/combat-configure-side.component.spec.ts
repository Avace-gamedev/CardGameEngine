import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CombatConfigureSideComponent } from './combat-configure-side.component';

describe('CombatCreationSideComponent', () => {
  let component: CombatConfigureSideComponent;
  let fixture: ComponentFixture<CombatConfigureSideComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CombatConfigureSideComponent],
    });
    fixture = TestBed.createComponent(CombatConfigureSideComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
