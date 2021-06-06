import { Component, OnInit } from '@angular/core';
import { InventoryApiClient, AuthorDto } from 'src/api-client/inventory-api-client';

@Component({
  selector: 'app-authors',
  templateUrl: './authors-list.component.html',
  styleUrls: ['./authors-list.component.css']
})
export class AuthorsListComponent implements OnInit {
  authors: AuthorDto[];

  constructor(public client: InventoryApiClient) { }

  ngOnInit() {
    this.client.authorsAll().subscribe((res) => {
        this.authors = res;
    });
  }
}
