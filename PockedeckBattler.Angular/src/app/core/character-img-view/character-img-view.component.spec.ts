import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CharacterImgViewComponent } from './character-img-view.component';

describe('CharacterImageViewComponent', () => {
  let component: CharacterImgViewComponent;
  let fixture: ComponentFixture<CharacterImgViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CharacterImgViewComponent],
    });
    fixture = TestBed.createComponent(CharacterImgViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
