import { httpResource } from '@angular/common/http';
import { Injectable, Service, Signal, inject } from '@angular/core';
import { API_BASE_URL } from '../../core/api-url.token';
import { VechileMake } from '../../Dtos/vehicle-make';
import { VehicleType } from '../../Dtos/vehicle-type';
import { VehicleModel } from '../../Dtos/vehicle-model';
import { ModelSearch } from '../../Dtos/model-search';
import { VehicleTypeModel } from '../../Dtos/vechile-model-type';

@Service({ autoProvided: false })
export class VehicleExplorerService {
  private readonly baseUrl = inject(API_BASE_URL);

  private readonly apiUrl = `${this.baseUrl}/VehicleExplorer`;

  readonly makesResource = httpResource<VechileMake[]>(
    () => `${this.apiUrl}/makes`
  );

  getMakeTypesResource(makeId: Signal<number | null>) {
    return httpResource<VehicleType[]>(() => {
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

  getModelsByCriteriaResource(modelSearch: Signal<ModelSearch | null>) {
    return httpResource<VehicleTypeModel[]>(() => {
      const criteria = modelSearch();

      if (!criteria) {
        return undefined;
      }

      const baseUrl =
        `${this.apiUrl}/make/${criteria.makeId}/modelyear/${criteria.year}`;

      return {
        url: criteria.type ? `${baseUrl}/type/${criteria.type}` : baseUrl,
        method: 'GET'
      };
    });
  }

}
