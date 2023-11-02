import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CombatJoinComponent } from './combat-join.component';

describe('JoinComponent', () => {
  let component: CombatJoinComponent;
  let fixture: ComponentFixture<CombatJoinComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CombatJoinComponent],
    });
    fixture = TestBed.createComponent(CombatJoinComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
