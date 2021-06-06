import { Component, OnInit, Input } from '@angular/core';
import { Book } from 'src/api-client/inventory-api-client';

@Component({
  selector: 'app-book-item-preview',
  templateUrl: './book-item-preview.component.html',
  styleUrls: ['./book-item-preview.component.css']
})
export class BookItemPreviewComponent implements OnInit {
  @Input() book: Book;

  constructor() { }

  ngOnInit() {
  }

}
