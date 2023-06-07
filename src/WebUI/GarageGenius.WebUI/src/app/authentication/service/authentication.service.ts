import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {environment} from 'src/environments/environment';
import {SignUpModel} from "../sign-up/models/sign-up.model";
import {SignInModel} from "../sign-in/models/sign-in.model";
import {AuthenticationResponseModel} from "../sign-in/models/authentication-response.model";


@Injectable({
  providedIn: 'root'
})
export class AuthenticationService implements IAuthenticationService {
  private _signUpPath: string = environment.signUpUrl;
  private _signInPath: string = environment.signInUrl;
  private basePath: string = environment.baseUrl;

  private constructor(private _httpClient: HttpClient) {
  }

  public signUpUser(signUpModel: SignUpModel): Observable<void> {
    return this._httpClient.post<void>(this.basePath + this._signUpPath, signUpModel);
  }

  public signInUser(signInModel: SignInModel): Observable<AuthenticationResponseModel> {
    return this._httpClient.post<AuthenticationResponseModel>(this.basePath + this._signInPath, signInModel);
  }

  public setAuthenticationToken(accessToken: string): void {
    localStorage.setItem('access-token', accessToken);
  }

  public getAuthenticationToken(): string {
    // TODO - change store jwt into cookie instead localStorage?
    return localStorage.getItem('access-token') as string;
  }

  public showMe(): Observable<unknown> {
    return this._httpClient.get<AuthenticationResponseModel>(this.basePath + `users-module/users/me`);
  }
}

export interface IAuthenticationService {
  signUpUser(signUpModel: SignUpModel): Observable<void>;
  signInUser(signInModel: SignInModel): Observable<AuthenticationResponseModel>;
  showMe(): Observable<unknown>;
  setAuthenticationToken(token: string): void;
  getAuthenticationToken(): string;
}