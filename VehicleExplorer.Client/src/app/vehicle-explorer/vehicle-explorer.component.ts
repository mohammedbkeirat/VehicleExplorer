
import { CommonModule } from '@angular/common';
import { Component, computed, inject, signal } from '@angular/core';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { VehicleExplorerService } from './vehicle-explorer.service';
import { Make } from '../Dtos/make';
import { MatSelectModule } from '@angular/material/select';
import { form, required } from '@angular/forms/signals';
import { Field, FormField } from '@angular/forms/signals';
import { ModelSearch } from '../Dtos/model-search';

@Component({
  selector: 'app-vehicle-explorer',
  standalone: true,
  imports: [
    MatAutocompleteModule,
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatProgressSpinnerModule,
    MatTableModule,
    MatTooltipModule,
    MatSelectModule, FormField
  ],
  templateUrl: './vehicle-explorer.component.html',
  styleUrl: './vehicle-explorer.component.scss',
  providers: [VehicleExplorerService]
})
export class VehicleExplorerComponent {

  private readonly vehicleService = inject(VehicleExplorerService);

  private readonly searchCriteria = signal<ModelSearch | null>(null);
  readonly submitted = signal(false);
  readonly isMakeDropdownOpen = signal(false);

  readonly submittedMakeId = computed(
    () => this.searchCriteria()?.makeId ?? null
  );

  readonly makesResource = this.vehicleService.makesResource;
  readonly modelsResource = this.vehicleService.getModelsByCriteriaResource(this.searchCriteria);
  readonly makeTypesResource = this.vehicleService.getMakeTypesResource(this.submittedMakeId);

  readonly vehicleSearchModel = signal({
    make: null as Make | null,
    year: '' as string,
    makeSearchText: ''
  });

  readonly vehicleSearchForm = form(
    this.vehicleSearchModel,
    path => {
      required(path.make);
      required(path.year);
    }
  );

  readonly filteredMakes = computed(() => {
    const makes = this.makesResource.value() ?? [];
    const searchText = this.vehicleSearchModel().makeSearchText?.trim().toLowerCase();

    if (!searchText) {
      return makes;
    }

    return makes.filter(make => make.Make_Name.toLowerCase().includes(searchText));
  });

  selectMake(make: Make) {
    this.vehicleSearchModel.update(model => ({
      ...model,
      make,
      makeSearchText: make.Make_Name
    }));

    this.isMakeDropdownOpen.set(false);
  }

  selectType(type: string) {
    this.searchCriteria.update(criteria =>
      criteria ? { ...criteria, type } : criteria
    );
  }

  readonly modelResults = computed(
    () => this.modelsResource.value() ?? []
  );

  toggleMakeDropdown(): void {
    this.isMakeDropdownOpen.update(open => !open);
  }

  onMakeInputFocus(): void {
    this.isMakeDropdownOpen.set(true);
  }

  onMakeInputBlur(): void {
    this.isMakeDropdownOpen.set(false);
  }

  onSearch(): void {
    this.submitted.set(true);

    if (this.vehicleSearchForm().invalid()) {
      return;
    }

    const { make, year } = this.vehicleSearchModel();

    if (!make || year === null) {
      return;
    }

    this.searchCriteria.set({
      makeId: make.Make_ID,
      year: Number(year),
      type: ''
    });
  }

  readonly currentYear = new Date().getFullYear();

  readonly years = Array.from(
    { length: this.currentYear - 1980 + 1 },
    (_, index) => this.currentYear - index
  );


}
