import { Component, computed, inject, signal } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { VehicleExplorerService } from './vehicle-explorer.service';
import { VechileMake } from '../../Dtos/vehicle-make';
import { form, required } from '@angular/forms/signals';
import { FormField } from '@angular/forms/signals';
import { ModelSearch } from '../../Dtos/model-search';
import { SearchableSelectComponent } from '../../shared/searchable-select/searchable-select.component';

@Component({
  selector: 'app-vehicle-explorer',
  standalone: true,
  imports: [
    MatIconModule,
    FormField,
    SearchableSelectComponent
  ],
  templateUrl: './vehicle-explorer.component.html',
  styleUrl: './vehicle-explorer.component.scss',
  providers: [VehicleExplorerService]
})
export class VehicleExplorerComponent {

  private readonly vehicleService = inject(VehicleExplorerService);

  private readonly searchCriteria = signal<ModelSearch | null>(null);
  readonly submitted = signal(false);

  readonly submittedMakeId = computed(
    () => this.searchCriteria()?.makeId ?? null
  );

  readonly makesResource = this.vehicleService.makesResource;
  readonly modelsResource = this.vehicleService.getModelsByCriteriaResource(this.searchCriteria);
  readonly makeTypesResource = this.vehicleService.getMakeTypesResource(this.submittedMakeId);

  readonly vehicleSearchModel = signal({
    make: null as VechileMake | null,
    year: null as number | null,
  });

  readonly vehicleSearchForm = form(
    this.vehicleSearchModel,
    path => {
      required(path.make);
      required(path.year);
    }
  );

  selectType(type: string) {
    this.searchCriteria.update(criteria =>
      criteria ? { ...criteria, type } : criteria
    );
  }

  readonly modelResults = computed(
    () => this.modelsResource.value() ?? []
  );

  onSearch(): void {
    this.submitted.set(true);

    if (this.vehicleSearchForm().invalid()) {
      return;
    }

    const model = this.vehicleSearchModel();

    if (!model.make || model.year === null) {
      return;
    }
     
    this.searchCriteria.set({
      makeId: model.make.makeId,
      year: model.year,
      type: ''
    });
  }

  readonly currentYear = new Date().getFullYear();

  readonly years = Array.from(
    { length: this.currentYear - 1980 + 1 },
    (_, index) => this.currentYear - index
  );

}
