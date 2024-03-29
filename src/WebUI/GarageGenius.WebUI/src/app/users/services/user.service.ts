import { Injectable } from '@angular/core';
import {BaseUserService} from "../models/base-user-service";
import {catchError, Observable, throwError} from "rxjs";
import {GetUserResponseModel, GetUsersResponseModel} from "../models/get-users-response-model";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import { environment } from '../../../environments/environment';
import {UserRequestModel, UserResponseModel} from "../models/user.model";

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseUserService {

  constructor(private _httpClient: HttpClient) {
    super();
  }

  override getUsers(): Observable<GetUsersResponseModel> {
    return this._httpClient
      .get<GetUsersResponseModel>(environment.getUsersUrl)
      .pipe(catchError(this.handleError))
  }

  override getUserById(userId: string): Observable<GetUserResponseModel> {
    return this._httpClient
      .get<GetUserResponseModel>(environment.getUserByIdUrl + userId)
      .pipe(catchError(this.handleError))
  }

  override getLoggedUser(): Observable<any> { // todo - define the type
      return this._httpClient
      .get<any>(environment.getLoggedUserUrl)
      .pipe(catchError(this.handleError));
  }

  postUser(user: UserRequestModel): Observable<UserResponseModel> {
    return this._httpClient
      .post<UserResponseModel>(environment.postUserUrl, user)
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${error.error.message}`;
    } else {
      errorMessage = `Server returned code: ${error.status}, error message is: ${error.message}`;
    }
    console.error(errorMessage);
    // TODO - add a remote logging service like in backend - Serilog with Seq sink
    return throwError(() => errorMessage);
  }
}
