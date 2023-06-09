import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  AuthenticationServiceBase,
  IAuthenticationService,
} from '../service/authentication.service';
import { catchError, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { SignInModel } from './models/sign-in.model';
import { AuthenticationResponseModel } from './models/authentication-response.model';
import { SignInFormModel } from './models/sign-in-form.model';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss'],
})
export class SignInComponent implements OnInit {
  private readonly _authenticationService: IAuthenticationService;
  private readonly _formBuilder: FormBuilder;

  constructor(
    formBuilder: FormBuilder,
    authenticationService: AuthenticationServiceBase
  ) {
    this._authenticationService = authenticationService;
    this._formBuilder = formBuilder;
  }

  ngOnInit(): void {
    this.signInForm = this._formBuilder.group({
      email: [
        `krystianpetek2@gmail.com`,
        {
          validators: [
            Validators.required,
            Validators.email,
            Validators.minLength(5),
            Validators.maxLength(60),
          ],
          nonNullable: false,
          asyncValidators: [],
          updateOn: `change`,
        },
      ],
      password: [
        `Password!23`,
        {
          validators: [
            Validators.required,
            Validators.minLength(8),
            Validators.maxLength(60),
          ],
          nonNullable: false,
        },
      ],
    });
  }

  public signInForm!: FormGroup<SignInFormModel>;
  public error?: string | null;

  public get email(): SignInFormModel['email'] {
    return this.signInForm.controls.email;
  }

  public get password(): SignInFormModel['password'] {
    return this.signInForm.controls.password;
    // TODO - its safety?
  }

  public signInResetForm(): void {
    this.signInForm.reset();
  }

  public signInSubmitForm(): void {
    this.error = null;

    const signInModel: SignInModel = this.signInForm.value as SignInModel;
    this._authenticationService
      .signInUser(signInModel)
      .pipe(
        catchError((err: HttpErrorResponse) => {
          let errorMessage = '';
          if (err.error instanceof ErrorEvent) {
            errorMessage = `An error occurred: ${err.error.message}`;
          } else {
            errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
          }
          this.error = err.error.detail;
          return throwError(errorMessage);
        })
      )
      .subscribe((response: AuthenticationResponseModel): void => {
        this._authenticationService.setAuthenticationToken(
          response.accessToken
        );

        this._authenticationService.setUserInfo(response);
      });
  }

  getMe(): void {
    this._authenticationService.showMe().subscribe((response: any) => {
      window.sessionStorage.setItem('user', JSON.stringify(response));
    });
  }
}
