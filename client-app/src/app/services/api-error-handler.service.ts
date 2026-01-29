import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import {ToastService} from './toast.service';

@Injectable()
export class ApiErrorHandlerService implements ErrorHandler {

  constructor(private injector: Injector) {}

  handleError(error: any): void {
    const toastService = this.injector.get(ToastService);

    if (error instanceof HttpErrorResponse) {

      switch (error.status) {
        case 403:
          toastService.error('Forbidden! You do not have access.');
          break;
        case 404:
          toastService.error('Resource not found.');
          break;
        case 400:
        case 409:
          toastService.error(error.error);
          break;
        case 500:
          toastService.error('Internal server error. Please try again later.');
          break;
      }
    } else {

      toastService.error(error.message || 'An unexpected error occurred.');
      console.error('Global error:', error);
    }
  }
}
