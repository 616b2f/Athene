import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthorsListComponent } from './authors-list/authors-list.component';
import { RouterModule, Routes } from '@angular/router';
import { CreateAuthorComponent } from './create-author/create-author.component';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { InventoryListComponent } from './inventory-list/inventory-list.component';
import { BookItemPreviewComponent } from './book-item-preview/book-item-preview.component';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { CreateBookComponent } from './create-book/create-book.component';

const routes: Routes = [
  {
    path: '',
    canActivate: [AuthorizeGuard],
    children: [
      { path: 'inventory/:q', component: InventoryListComponent },
      { path: 'inventory', component: InventoryListComponent },
      { path: 'authors', component: AuthorsListComponent },
      { path: 'authors/create', component: CreateAuthorComponent },
      { path: 'books/create', component: CreateBookComponent },
    ],
  },
];

@NgModule({
  declarations: [
    AuthorsListComponent,
    CreateAuthorComponent,
    InventoryListComponent,
    BookItemPreviewComponent,
    CreateBookComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NgbDropdownModule,
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class LibrarianModule { }
