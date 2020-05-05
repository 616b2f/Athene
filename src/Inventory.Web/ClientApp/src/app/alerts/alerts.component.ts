import { Component, OnInit } from '@angular/core';
import { AlertService, Alert } from 'src/services/alert/alert.service';
import { debounce } from 'rxjs/operators';

@Component({
  selector: 'app-alerts',
  templateUrl: './alerts.component.html',
  styleUrls: ['./alerts.component.css']
})
export class AlertsComponent implements OnInit {
  alerts: Alert[] = [];

  constructor(private alertService: AlertService) { }

  ngOnInit() {
    this.alertService.onAlert
    .subscribe(alert => {
      setTimeout(() => this.close(alert), alert.debounceTime);
      this.alerts.push(alert);
    });
  }

  close(alert: Alert) {
    this.alerts.splice(this.alerts.indexOf(alert), 1);
  }
}
