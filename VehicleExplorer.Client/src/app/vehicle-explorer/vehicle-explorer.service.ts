import { httpResource } from '@angular/common/http';
import { Injectable, Service, Signal, inject } from '@angular/core';
import { API_BASE_URL } from '../core/api-url.token';
import { Make } from '../Dtos/make';
import { MakeType } from '../Dtos/make-type';
import { MakeModel } from '../Dtos/vehicle-model';
import { ModelSearch } from '../Dtos/model-search';

// import { MakeDto } from '../models/make.dto';

@Service({ autoProvided: false })
export class VehicleExplorerService {
  private readonly baseUrl = inject(API_BASE_URL);

  private readonly apiUrl = `${this.baseUrl}/VehicleExplorer`;

  readonly makesResource = httpResource<Make[]>(
    () => `${this.apiUrl}/makes`
  );

  getMakeTypesResource(makeId: Signal<number | null>) {
    return httpResource<MakeType[]>(() => {
      const id = makeId();

      if (id === null) {
        return undefined;
      }

      return {
        url: `${this.apiUrl}/make/${id}/types`,
        method: 'GET',
      };
    });
  }

  getModelByMakeAndYearResource(modelSearch: Signal<ModelSearch | null>) {
    return httpResource<MakeModel[]>(() => {
      const criteria = modelSearch();

      if (!criteria) {
        return undefined;
      }

      return {
        url: `${this.apiUrl}/make/${criteria.makeId}/modelyear/${criteria.year}`,
        method: 'GET',
      };
    });
  }
}
