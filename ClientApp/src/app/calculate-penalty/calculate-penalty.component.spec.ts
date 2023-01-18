import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CalculatePenaltyComponent } from './calculate-penalty.component';
import { ApiService } from '../services/api.service';
import { of } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { CountrySettings } from '../models/countrySettings.model';
import { ApiMockService } from '../mocks/api.service.mock';

describe('CalculatePenaltyComponent', () => {
  let component: CalculatePenaltyComponent;
  let fixture: ComponentFixture<CalculatePenaltyComponent>;
  let apiService: ApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CalculatePenaltyComponent],
      imports: [FormsModule],
      providers: [
        { provide: ApiService, useClass: ApiMockService }
      ]
    });
    fixture = TestBed.createComponent(CalculatePenaltyComponent);
    component = fixture.componentInstance;
    apiService = TestBed.get(ApiService);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call apiService.getCountrySettings on ngOnInit & set countrySettings with the data from the api', () => {
    fixture.detectChanges();
    apiService.getCountrySettings().subscribe(countrySettings => {
      expect(component.countrySettings).toEqual(countrySettings);
    });
  });

  it('should set error message when returnDate is less than checkoutDate', () => {
    component.checkoutDate = new Date();
    component.returnDate = new Date(new Date().setDate(new Date().getDate() - 1));
    component.calculatePenalty();
    expect(component.isError).toBe(true);
    expect(component.message).toEqual('Return date must be greater than checkout date.');
  });

  it('should set error message when selectedCountry is null or empty', () => {
    component.selectedCountry = '';
    component.calculatePenalty();
    expect(component.isError).toBe(true);
    expect(component.message).toEqual('Please select country.');
  });

  it('should set apiResult when calculatePenalty is called', () => {
    component.selectedCountry = '1';
    component.checkoutDate = new Date();
    component.returnDate = new Date(new Date().setDate(new Date().getDate() + 1));
    component.calculatePenalty();
    expect(component.apiResult).toEqual({ penalty: 10, days: [new Date()] });
  });
});
