import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CombatSideCommonElementsComponent } from './combat-side-common-elements.component';

describe('CombatSideComponent', () => {
  let component: CombatSideCommonElementsComponent;
  let fixture: ComponentFixture<CombatSideCommonElementsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CombatSideCommonElementsComponent],
    });
    fixture = TestBed.createComponent(CombatSideCommonElementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
