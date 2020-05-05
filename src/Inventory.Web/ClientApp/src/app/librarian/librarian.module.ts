import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthorsListComponent } from './authors-list/authors-list.component';
import { RouterModule, Routes } from '@angular/router';
import { CreateAuthorComponent } from './create-author/create-author.component';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { ReactiveFormsModule } from '@angular/forms';

const routes: Routes = [
  {
    path: '',
    component: AuthorsListComponent
  },
  {
    path: 'authors',
    component: AuthorsListComponent,
    canActivate: [AuthorizeGuard]
  },
  { path: 'authors/create', component: CreateAuthorComponent, canActivate: [AuthorizeGuard] },
];

@NgModule({
  declarations: [AuthorsListComponent, CreateAuthorComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class LibrarianModule { }
