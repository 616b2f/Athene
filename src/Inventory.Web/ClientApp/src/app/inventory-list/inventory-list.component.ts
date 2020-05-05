import { Component, OnInit } from '@angular/core';
import { InventoryApiClient, Article, InventoryItem } from 'src/api-client/inventory-api-client';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-inventory-list',
  templateUrl: './inventory-list.component.html',
  styleUrls: ['./inventory-list.component.css']
})
export class InventoryListComponent implements OnInit {
  items$: Observable<Article[]>;

  constructor(private client: InventoryApiClient,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.items$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
        this.client.inventoryAll(params.get('q')))
    );
  }

  notBorrowedCount(inventoryItems: InventoryItem[]): number {
    return inventoryItems.filter(i => !i.rentedBy).length;
  }

  borrowedCount(inventoryItems: InventoryItem[]): number {
    return inventoryItems.filter(i => i.rentedBy).length;
  }
}
