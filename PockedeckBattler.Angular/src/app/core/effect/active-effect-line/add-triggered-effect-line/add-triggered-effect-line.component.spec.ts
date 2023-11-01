import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTriggeredEffectLineComponent } from './add-triggered-effect-line.component';

describe('AddTriggeredEffectViewComponent', () => {
  let component: AddTriggeredEffectLineComponent;
  let fixture: ComponentFixture<AddTriggeredEffectLineComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddTriggeredEffectLineComponent],
    });
    fixture = TestBed.createComponent(AddTriggeredEffectLineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
