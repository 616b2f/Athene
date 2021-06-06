import { Component, OnInit } from '@angular/core';
import { InventoryApiClient, InventoryItem, Book } from 'src/api-client/inventory-api-client';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-inventory-list',
  templateUrl: './inventory-list.component.html',
  styleUrls: ['./inventory-list.component.css']
})
export class InventoryListComponent implements OnInit {
  items$: Observable<Book[]>;
  searchTargets: string[];

  constructor(private client: InventoryApiClient,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.searchTargets = [
      'All',
      'Books',
      'Magazines',
    ];
  }

  onSearch(value: string) {
    if (value) {
      this.items$ = this.client.inventoryAll(value);
    } else {
      this.items$ = undefined;
    }
  }

  isBook(item): boolean {
    return item instanceof Book;
  }

  notBorrowedCount(inventoryItems: InventoryItem[]): number {
    return inventoryItems.filter(i => !i.rentedBy).length;
  }

  borrowedCount(inventoryItems: InventoryItem[]): number {
    return inventoryItems.filter(i => i.rentedBy).length;
  }
}
