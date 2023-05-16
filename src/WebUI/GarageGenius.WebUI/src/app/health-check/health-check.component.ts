import { Component } from '@angular/core';
import { HealthCheckService } from '../health-check.service';

@Component({
  selector: 'app-health-check',
  templateUrl: './health-check.component.html',
  styleUrls: ['./health-check.component.css'],
})
export class HealthCheckComponent {
  private healthCheckService: HealthCheckService;
  public moduleHealths: Record<Modules, HealthCheckDisplay> = {
    Cars: {
      module: { message: 'Loading...' },
      name: 'Cars',
    },
    Customers: {
      module: { message: 'Loading...' },
      name: 'Customers',
    },
    Notifications: {
      module: { message: 'Loading...' },
      name: 'Notifications',
    },
    Users: {
      module: { message: 'Loading...' },
      name: 'Users',
    },
  };

  constructor(healthCheckService: HealthCheckService) {
    this.healthCheckService = healthCheckService;
    this.healthCheckService.healthCheckCars().subscribe({
      next: (response: HealthCheck) => {
        this.moduleHealths.Cars.module = response;
      },
      error: (error: void) => {
        this.moduleHealths.Cars.module = {
          message: 'Cars module is not available',
        };
      },
    });
    this.healthCheckService.healthCheckCustomers().subscribe({
      next: (response: HealthCheck) => {
        this.moduleHealths.Customers.module = response;
      },
      error: (error: void) => {
        this.moduleHealths.Customers.module = {
          message: 'Customers module is not available',
        };
      },
    });
    this.healthCheckService.healthCheckUsers().subscribe({
      next: (response: HealthCheck) => {
        this.moduleHealths.Users.module = response;
      },
      error: (error: void) => {
        this.moduleHealths.Users.module = {
          message: 'Users module is not available',
        };
      },
    });
    this.healthCheckService.healthCheckNotifications().subscribe({
      next: (response: HealthCheck) => {
        this.moduleHealths.Notifications.module = response;
      },
      error: (error: void) => {
        this.moduleHealths.Notifications.module = {
          message: 'Notifications module is not available',
        };
      },
    });
  }
}
export interface HealthCheck {
  message: string;
}

export interface HealthCheckDisplay {
  module: HealthCheck;
  name: string;
}

export type Modules = 'Users' | 'Cars' | 'Customers' | 'Notifications';
// https://www.typescriptlang.org/docs/handbook/utility-types.html#recordkeys-type
// https://blog.angular-university.io/rxjs-error-handling/