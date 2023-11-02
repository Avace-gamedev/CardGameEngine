import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OpponentSideElementsComponent } from './opponent-side-elements.component';

describe('OpponentSideElementsComponent', () => {
  let component: OpponentSideElementsComponent;
  let fixture: ComponentFixture<OpponentSideElementsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OpponentSideElementsComponent]
    });
    fixture = TestBed.createComponent(OpponentSideElementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
