import { TestBed } from '@angular/core/testing';

import { TeslaDataService } from './tesla-data.service';

describe('TeslaDataService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TeslaDataService = TestBed.get(TeslaDataService);
    expect(service).toBeTruthy();
  });
});
