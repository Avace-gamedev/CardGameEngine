import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayerSideElementsComponent } from './player-side-elements.component';

describe('PlayerSideElementsComponent', () => {
  let component: PlayerSideElementsComponent;
  let fixture: ComponentFixture<PlayerSideElementsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PlayerSideElementsComponent]
    });
    fixture = TestBed.createComponent(PlayerSideElementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
