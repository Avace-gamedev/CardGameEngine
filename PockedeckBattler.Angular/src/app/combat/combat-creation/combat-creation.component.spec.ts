import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CombatCreationComponent } from './combat-creation.component';

describe('CombatCreationComponent', () => {
  let component: CombatCreationComponent;
  let fixture: ComponentFixture<CombatCreationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CombatCreationComponent]
    });
    fixture = TestBed.createComponent(CombatCreationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
