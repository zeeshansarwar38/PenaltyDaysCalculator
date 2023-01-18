import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CountrySettings } from '../models/countrySettings.model';
import { CalculatePenalty } from '../models/calculatePenalty.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  public baseUrl = '';
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getCountrySettings() {
    return this.http.get<CountrySettings[]>(this.baseUrl + 'api/countrysettings');
  }

  calculatePenalty(request: CalculatePenalty):any {
    return this.http.post<CountrySettings[]>(this.baseUrl + 'api/penaltycalculator', request);
  }
}
