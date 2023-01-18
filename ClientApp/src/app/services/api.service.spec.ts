import { TestBed, async, inject } from '@angular/core/testing';
import { HttpClientModule, HttpClient } from '@angular/common/http'
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ApiService } from './api.service';
import { InjectionToken } from '@angular/core';
import { CalculatePenalty } from '../models/calculatePenalty.model';
export const BASE_URL = 'http://localhost/';

describe('ApiService', () => {
  let apiService: ApiService;
  let httpMock: HttpTestingController;
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
      ],
      providers: [
        { provide: 'BASE_URL', useValue: 'http://localhost/' },
        ApiService,
        HttpClient
      ],
    });
    apiService = TestBed.get(ApiService);
    httpMock = TestBed.get(HttpTestingController);
  });
  it('should be created', () => {
    const service: ApiService = TestBed.get(ApiService);
    expect(service).toBeTruthy();
  });
  it(`should fetch country settings`, async(inject([HttpTestingController, ApiService],
    (httpClient: HttpTestingController, apiService: ApiService) => {
      const countrySetting = [
        { id: '2e51ed7c-b6d8-4b1a-b548-75fa729d8fxc', countryName: 'United States', currencyCode: 'USD', penaltyAmount: '5.00', weekendDays: 'Saturday,Sunday' },
        { id: '2e51ed7c-b6d8-4b1a-b548-75fa729d8fdb', countryName: 'Dubai', currencyCode: 'AED', penaltyAmount: '5.00', weekendDays: 'Friday,Saturday' },
        { id: '2e51ed7c-b6d8-4b1a-b548-75fa723d8fdc', countryName: 'Turkey', currencyCode: 'TRY', penaltyAmount: '10.00', weekendDays: 'Saturday,Sunday' },
      ];

      apiService.getCountrySettings()
        .subscribe((countrtSettings: any) => {
          expect(countrtSettings.length).toBe(3);
        });
      let req = httpMock.expectOne(BASE_URL.toString() + 'api/countrysettings');
      expect(req.request.method).toBe("GET");
      req.flush(countrySetting);
      httpMock.verify();
    })));

  it(`should fetch calculated penalty`, async(inject([HttpTestingController, ApiService],
    (httpClient: HttpTestingController, apiService: ApiService) => {
      const penaltyResponse = { penalty: 5, days: [new Date()] };
      const penaltyRequest: CalculatePenalty = { checkoutDate: new Date(), returnDate: new Date(new Date().setDate(new Date().getDate() + 1)), selectedCountry: '2e51ed7c-b6d8-4b1a-b548-75fa729d8fxc' };
      apiService.calculatePenalty(penaltyRequest)
        .subscribe((serverResponse: any) => {
          expect(serverResponse).toEqual(penaltyResponse);
        });
      let req = httpMock.expectOne(BASE_URL.toString() + 'api/penaltycalculator');
      expect(req.request.method).toBe("POST");
      req.flush(penaltyResponse);
      httpMock.verify();
    })));
});
