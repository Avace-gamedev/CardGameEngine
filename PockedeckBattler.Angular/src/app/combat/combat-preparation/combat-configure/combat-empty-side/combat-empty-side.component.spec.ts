import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CombatEmptySideComponent } from './combat-empty-side.component';

describe('CombatEmptySideComponent', () => {
  let component: CombatEmptySideComponent;
  let fixture: ComponentFixture<CombatEmptySideComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CombatEmptySideComponent]
    });
    fixture = TestBed.createComponent(CombatEmptySideComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
