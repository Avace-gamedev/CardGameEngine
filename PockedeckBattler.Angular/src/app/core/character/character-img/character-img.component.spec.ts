import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CharacterImgComponent } from './character-img.component';

describe('CharacterImageViewComponent', () => {
  let component: CharacterImgComponent;
  let fixture: ComponentFixture<CharacterImgComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CharacterImgComponent],
    });
    fixture = TestBed.createComponent(CharacterImgComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
