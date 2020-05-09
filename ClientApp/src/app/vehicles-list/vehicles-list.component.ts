import { Component, OnInit } from '@angular/core';
import { Vehicle, KeyValuePair } from '../models/vehicle';
import { VehicleService } from '../services/vehicle.service';

@Component({
  selector: 'app-vehicles-list',
  templateUrl: './vehicles-list.component.html',
  styleUrls: ['./vehicles-list.component.css']
})
export class VehiclesListComponent implements OnInit {
  private readonly PAGE_SIZE = 5;
  queryResult: any = {};
  makes: KeyValuePair[];
  query: any = {
    pageSize: this.PAGE_SIZE
  };
  columns = [
    { title: 'Id'},
    { title: 'Make', key: 'make', isSortable: true },
    { title: 'Model', key: 'model', isSortable: true },
    { title: 'Contact Name', key: 'contactName', isSortable: true },
    {}
  ]


  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {
    this.vehicleService.getMakes()
      .subscribe(makes => this.makes = makes);
    this.populateVehicles();


  }
  private populateVehicles() {
    this.vehicleService.getVehicles(this.query)
      .subscribe(result => this.queryResult = result);
  }
  onFilterChange() {
    this.query.page = 1;
    this.populateVehicles();
  }
  resetFilter() {
    this.query = {
      page: 1,
      pageSize: this.PAGE_SIZE       
    };
    this.populateVehicles();
  }
  sortBy(columnName) {
    if (this.query.sortBy === columnName) {
      this.query.IsSortAscending = !this.query.IsSortAscending;
    }
    else {
      this.query.sortBy = columnName;
      this.query.IsSortAscending = true;
    }
    this.populateVehicles();
  }

  onPageChange(page) {
    this.query.page = page;
    this.populateVehicles();
  }
}
