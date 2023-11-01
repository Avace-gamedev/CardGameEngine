import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardDamageComponent } from './card-damage.component';

describe('CardDamageComponent', () => {
  let component: CardDamageComponent;
  let fixture: ComponentFixture<CardDamageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CardDamageComponent]
    });
    fixture = TestBed.createComponent(CardDamageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
