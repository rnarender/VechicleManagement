import * as Sentry from "@sentry/browser";

Sentry.init({
  dsn: "https://eace9b1f9b994ab1b84410b068e51bad@sentry.io/5191011"
});
import { FormsModule } from '@angular/forms';
import { NgModule, ErrorHandler } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { VehicleFormComponent } from './vehicle-form/vehicle-form.component';
import { HttpModule } from '@angular/http';
import { VehicleService } from './services/vehicle.service';
import { AppErrorHandler } from './app.error-handler';

import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ToastaModule } from 'ngx-toasta';
//import { VehicleListComponent } from "./vehicle-list/vehicle-list";
import { VehiclesListComponent } from './vehicles-list/vehicles-list.component';
import { PaginationComponent } from "./shared/pagination.component";
import { ViewVehicleComponent } from './view-vehicle/view-vehicle.component';
import { PhotoService } from "./services/photo.service";
import { BrowserXhr } from '@angular/http';
import { BrowserXhrWithProgress, ProgressService } from "./services/progress.service";



@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    VehicleFormComponent,
   // VehicleListComponent,
    VehiclesListComponent,
    PaginationComponent,
    ViewVehicleComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    CommonModule,
    BrowserAnimationsModule, // required animations module
    HttpModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'vehicles', pathMatch: 'full' },
      { path: 'vehicles/new', component: VehicleFormComponent },
      { path: 'vehicles/edit/:id', component: VehicleFormComponent },
      { path: 'vehicles/:id', component: ViewVehicleComponent},
      { path: 'vehicles', component: VehiclesListComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: '**', redirectTo: 'home' }
 ]),
    ToastaModule.forRoot()
  ],
  providers: [
    { provide: ErrorHandler, useClass: AppErrorHandler },
    { provide: BrowserXhr, useClass: BrowserXhrWithProgress },
    VehicleService,
    PhotoService,
    ProgressService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
