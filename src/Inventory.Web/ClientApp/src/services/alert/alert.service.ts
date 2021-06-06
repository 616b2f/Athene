import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

export interface Alert {
  type: string;
  message: string;
  debounceTime: number;
}

@Injectable({
  providedIn: 'root'
})
export class AlertService {
  private defaultDebaunceTime = 5000;
  onAlert = new Subject<Alert>();

  constructor() {}

  success(message: string) {
    this.onAlert.next({type: 'success', message: message, debounceTime: this.defaultDebaunceTime});
  }

  error(message: string) {
    this.onAlert.next({type: 'error', message: message, debounceTime: this.defaultDebaunceTime});
  }

  info(message: string) {
    this.onAlert.next({type: 'info', message: message, debounceTime: this.defaultDebaunceTime});
  }
}
