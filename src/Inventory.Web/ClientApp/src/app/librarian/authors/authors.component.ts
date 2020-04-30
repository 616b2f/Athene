import { Component, OnInit } from '@angular/core';
import { InventoryApiClient, AuthorDto } from 'src/api-client/inventory-api-client';

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.css']
})
export class AuthorsComponent implements OnInit {
  authors: AuthorDto[];

  constructor(public client: InventoryApiClient) { }

  ngOnInit() {
    this.client.authorsAll().subscribe((res) => {
        this.authors = res;
    });
  }
}
