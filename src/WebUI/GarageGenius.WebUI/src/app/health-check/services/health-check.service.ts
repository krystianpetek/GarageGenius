import { Injectable } from '@angular/core';
import { Observable, map, catchError, throwError, retry } from 'rxjs';
import {
  HttpClient,
  HttpErrorResponse,
  HttpStatusCode,
} from '@angular/common/http';
import { HealthCheckResponseModel } from '../models/health-check-response.model';

export interface IHealthCheckService {
  healthCheckUsers(): Observable<HealthCheckResponseModel>;
  healthCheckCustomers(): Observable<HealthCheckResponseModel>;
  healthCheckVehicles(): Observable<HealthCheckResponseModel>;
  healthCheckNotifications(): Observable<HealthCheckResponseModel>;
  healthCheckReservations(): Observable<HealthCheckResponseModel>;
}

@Injectable({
  providedIn: 'root',
})
export class HealthCheckService implements IHealthCheckService {
  private httpClient: HttpClient;
  private users = `health/users-module`;
  private customers = `health/customers-module`;
  private vehicles = `health/vehicles-module`;
  private notifications = `health/notifications-module`;
  private reservations = `health/reservations-module`;

  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
  }

  public healthCheckUsers(): Observable<HealthCheckResponseModel> {
    return this.handleRequest(this.users);
  }
  public healthCheckCustomers(): Observable<HealthCheckResponseModel> {
    return this.handleRequest(this.customers);
  }
  public healthCheckVehicles(): Observable<HealthCheckResponseModel> {
    return this.handleRequest(this.vehicles);
  }
  public healthCheckNotifications(): Observable<HealthCheckResponseModel> {
    return this.handleRequest(this.notifications);
  }
  public healthCheckReservations(): Observable<HealthCheckResponseModel> {
    return this.handleRequest(this.reservations);
  }

  private handleRequest(url: string): Observable<HealthCheckResponseModel> {
    return this.httpClient.get<HealthCheckResponseModel>(url).pipe(
      map((response) => response),
      retry(2),
      catchError(this.handleError)
    );
  }
  private handleError(error: HttpErrorResponse) {
    switch (error.status) {
      case 0:
        console.error('Client error:', error.error);
        break;
      case HttpStatusCode.InternalServerError:
        console.error('Server error:', error.error);
        break;
      case HttpStatusCode.BadRequest:
        console.error('Request error:', error.error);
        break;
      default:
        console.error('Unknown error:', error.error);
        break;
    }
    return throwError(() => error);
  }
}
