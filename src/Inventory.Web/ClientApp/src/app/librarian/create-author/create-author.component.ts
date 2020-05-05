import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { InventoryApiClient, CreateAuthorDto } from 'src/api-client/inventory-api-client';
import { AlertService } from 'src/services/alert/alert.service';

@Component({
  selector: 'app-create-author',
  templateUrl: './create-author.component.html',
  styleUrls: ['./create-author.component.css']
})
export class CreateAuthorComponent implements OnInit {
  public createForm;

  constructor(private formBuilder: FormBuilder,
              private inventoryApiClient: InventoryApiClient,
              private alertService: AlertService) { }

  ngOnInit() {
    this.createForm = this.formBuilder.group({
      fullName: '',
      info: ''
    });
  }

  create(authorData: CreateAuthorDto) {
    this.inventoryApiClient.authors(authorData)
      .subscribe((res) => {
        this.alertService.success('Author: ' + res.fullName + ' erfolgreich angelegt.');
      }
      , error => {
        this.alertService.error('Fehler aufgetreten beim anlegen von Author.');
      });
  }
}
