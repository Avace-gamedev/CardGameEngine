import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RandomEffectLineComponent } from './random-effect-line.component';

describe('RandomEffectLineComponent', () => {
  let component: RandomEffectLineComponent;
  let fixture: ComponentFixture<RandomEffectLineComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RandomEffectLineComponent]
    });
    fixture = TestBed.createComponent(RandomEffectLineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
