import { Injectable, Inject } from '@angular/core';
import { CountrySettings } from '../models/countrySettings.model';
import { CalculatePenalty } from '../models/calculatePenalty.model';
import { of } from 'rxjs/internal/observable/of';

@Injectable({
  providedIn: 'root'
})
export class ApiMockService {
  constructor() {
  }

  getCountrySettings() {
    const countrySettings: CountrySettings = { id: '1', countryName: 'United States', currencyCode: 'USD', penaltyAmount: '5.00', weekendDays: 'Saturday,Sunday' };
    return of([countrySettings]);
  }

  calculatePenalty(request: CalculatePenalty):any {
    return of({ penalty: 10, days: [new Date()] });
  }
}
