import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CombatCreationSideComponent } from './combat-creation-side.component';

describe('CombatCreationSideComponent', () => {
  let component: CombatCreationSideComponent;
  let fixture: ComponentFixture<CombatCreationSideComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CombatCreationSideComponent]
    });
    fixture = TestBed.createComponent(CombatCreationSideComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
