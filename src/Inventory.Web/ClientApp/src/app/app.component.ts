import { Component, OnInit } from '@angular/core';
import { AlertService } from 'src/services/alert/alert.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';

  constructor(alertService: AlertService) {
  }

  ngOnInit() {
    
  }
}
