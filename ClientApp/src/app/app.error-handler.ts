import * as Sentry from "@sentry/browser";
import { ErrorHandler, Inject, NgZone, isDevMode } from "@angular/core";
import { ToastaService, ToastaConfig } from "ngx-toasta";

export class AppErrorHandler implements ErrorHandler {
  constructor(
    private ngZone: NgZone,
    @Inject(ToastaService) private toastaService: ToastaService,
    private toastaConfig: ToastaConfig) {
    this.toastaConfig.theme = 'bootstrap';
  }
  handleError(error) {
    this.ngZone.run(() => {
      this.toastaService.error({
        title: 'Error',
        msg: 'An unxpected error occured',
        theme: 'bootstrap',
        showClose: true,
        timeout: 5000
      });
    });

    //if (!isDevMode())
    //  Sentry.captureException(error.originalError || error);
    //else
    //  throw error;
  }
}
