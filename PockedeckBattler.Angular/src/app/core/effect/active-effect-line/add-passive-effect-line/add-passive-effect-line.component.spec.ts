import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPassiveEffectLineComponent } from './add-passive-effect-line.component';

describe('AddPassiveEffectLineComponent', () => {
  let component: AddPassiveEffectLineComponent;
  let fixture: ComponentFixture<AddPassiveEffectLineComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddPassiveEffectLineComponent]
    });
    fixture = TestBed.createComponent(AddPassiveEffectLineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
