import { Component } from '@angular/core';
import { CalculatePenalty } from '../models/calculatePenalty.model';
import { CountrySettings } from '../models/countrySettings.model';
import { PenaltyResponse } from '../models/penaltyResponse.model';

import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-calculate-penalty',
  templateUrl: './calculate-penalty.component.html'
})
export class CalculatePenaltyComponent {
  public countrySettings: CountrySettings[] = [];
  public selectedCountrySettings: CountrySettings | null = null;
  public checkoutDate: Date = new Date();
  public returnDate: Date = new Date();
  public selectedCountry: string = '';
  public btnLabel: string = 'Calculate';
  public message: string = '';
  public isError: boolean = false;
  public apiResult: PenaltyResponse | null = null;

  constructor(private readonly apiService: ApiService) {
    this.apiService.getCountrySettings().subscribe(result => {
      this.countrySettings = result;
    }, error => {
      this.isError = true;
      this.message = error?.error.message
    });
  }

  calculatePenalty() {

    // Validation logic
    if (this.returnDate < this.checkoutDate) {
      this.message = 'Return date must be greater than checkout date.';
      this.isError = true;
      return;
    }
    if (this.selectedCountry == null || this.selectedCountry == '') {
      this.message = 'Please select country.';
      this.isError = true;
      return;
    }

    this.message = '';
    this.btnLabel = 'Calculating...';
    this.selectedCountrySettings = this.countrySettings.filter(itm => itm.id == this.selectedCountry)[0];

    // Preparing Request object
    const request: CalculatePenalty = {
      selectedCountry: this.selectedCountry,
      checkoutDate: this.checkoutDate,
      returnDate: this.returnDate
    }
    this.apiResult = null;

    // Calling backend api
    this.apiService.calculatePenalty(request).subscribe((result: PenaltyResponse) => {
      this.btnLabel = 'Calculate';
      this.isError = false;
      if (result.penalty > 0) {
        this.apiResult = result;
        this.message = '';
      } else {
        this.apiResult = null;
        this.message = 'No Penalty because book is returned within 10 business days.';
      }
    }, (error: any) => {
      this.apiResult = null;
      this.btnLabel = 'Calculate';
      this.isError = true;
      this.message = error?.error.message
    });
  }
}

