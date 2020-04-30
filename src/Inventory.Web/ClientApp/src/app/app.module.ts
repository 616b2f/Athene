import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { INVENTORY_API_BASE_URL, InventoryApiClient } from 'src/api-client/inventory-api-client';
import { InventoryListComponent } from './inventory-list/inventory-list.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    InventoryListComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'inventory/:q', component: InventoryListComponent },
      {
        path: 'librarian/authors',
        loadChildren: () => import('./librarian/authors/authors.module').then(mod => mod.AuthorsModule),
        canActivate: [AuthorizeGuard]
      },
      // { path: 'counter', component: CounterComponent },
      // { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },
    ]),
    NgbDropdownModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    // { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    { provide: INVENTORY_API_BASE_URL, useValue: 'https://localhost:5001' },
    InventoryApiClient
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
