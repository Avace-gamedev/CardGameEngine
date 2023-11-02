import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CombatConfigureComponent } from './combat-configure.component';

describe('CombatCreationComponent', () => {
  let component: CombatConfigureComponent;
  let fixture: ComponentFixture<CombatConfigureComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CombatConfigureComponent],
    });
    fixture = TestBed.createComponent(CombatConfigureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
