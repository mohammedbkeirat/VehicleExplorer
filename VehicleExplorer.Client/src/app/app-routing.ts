import { Routes } from '@angular/router';
import { VehicleExplorerComponent } from './features/vehicle-explorer/vehicle-explorer.component';

export const routes: Routes = [
  { path: "", redirectTo: '/vehicle-explorer', pathMatch: 'full' },
  { path: "vehicle-explorer", component: VehicleExplorerComponent },
  { path: "**", component: VehicleExplorerComponent },
];
