import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CombatCharacterImgComponent } from './combat-character-img.component';

describe('CombatCharacterImgComponent', () => {
  let component: CombatCharacterImgComponent;
  let fixture: ComponentFixture<CombatCharacterImgComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CombatCharacterImgComponent]
    });
    fixture = TestBed.createComponent(CombatCharacterImgComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
