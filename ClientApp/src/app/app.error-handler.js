"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var AppErrorHandler = /** @class */ (function () {
    function AppErrorHandler(toastaService, toastaConfig) {
        this.toastaService = toastaService;
        this.toastaConfig = toastaConfig;
        this.toastaConfig.theme = 'bootstrap';
    }
    AppErrorHandler.prototype.handleError = function (error) {
        console.log("ERROR123");
        this.toastaService.error({
            title: 'Error',
            msg: 'An unxpected error occured',
            theme: 'bootstrap',
            showClose: true,
            timeout: 5000
        });
    };
    return AppErrorHandler;
}());
exports.AppErrorHandler = AppErrorHandler;
//# sourceMappingURL=app.error-handler.js.map