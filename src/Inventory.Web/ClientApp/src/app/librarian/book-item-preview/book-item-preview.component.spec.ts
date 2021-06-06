import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookItemPreviewComponent } from './book-item-preview.component';

describe('BookItemPreviewComponent', () => {
  let component: BookItemPreviewComponent;
  let fixture: ComponentFixture<BookItemPreviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookItemPreviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookItemPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
