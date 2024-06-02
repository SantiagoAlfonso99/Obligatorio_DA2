import { Component, OnInit } from '@angular/core';
import { BuildingService } from 'src/app/services/building.service';
import { ManagerService } from 'src/app/services/manager.service';
import { BuildingReturnModel, ManagerReturnModel } from 'src/app/services/types';

@Component({
  selector: 'app-create-building',
  templateUrl: './create-building.component.html',
  styleUrls: ['./create-building.component.css']
})
export class CreateBuildingComponent implements OnInit{
  name: string = '';
  address: string = '';
  latitude :number = 0;
  longitude : number = 0;
  commonExpenses: number = 0;
  selectedBuildingId : number = 0;
  buildings : BuildingReturnModel[] = [];
  selectedManagerId : number = 0;
  managers : ManagerReturnModel[] = []
  constructor(
    private buildingService: BuildingService,
    private managerService: ManagerService,
  ) {}

  ngOnInit(): void {
    this.getAllBuildings();
    this.getAllManagers();
  }

  createBuilding() {
    this.buildingService.createBuilding(this.name, this.address, this.latitude, this.longitude, this.commonExpenses).subscribe((response) => {
    });
  }

  getAllBuildings(){
    this.buildingService.getAllBuildings().subscribe((response) => {
      this.buildings = response;
    });
  }

  getAllManagers(){
    this.managerService.getAllManagers().subscribe((response) => {
      this.managers = response;
    });
  }

  modifyManager(selectedManagerId : number, selectedBuildingId : number){
    this.buildingService.modifyBuildingManager(selectedManagerId, selectedBuildingId).subscribe((response) => {
    });
  }
}
