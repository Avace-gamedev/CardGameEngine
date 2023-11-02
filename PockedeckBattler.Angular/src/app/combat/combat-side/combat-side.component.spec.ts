import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CombatSideComponent } from './combat-side.component';

describe('CombatSideComponent', () => {
  let component: CombatSideComponent;
  let fixture: ComponentFixture<CombatSideComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CombatSideComponent]
    });
    fixture = TestBed.createComponent(CombatSideComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
