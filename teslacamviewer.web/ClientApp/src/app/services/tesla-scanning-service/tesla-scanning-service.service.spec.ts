import { TestBed } from '@angular/core/testing';

import { TeslaScanningServiceService } from './tesla-scanning-service.service';

describe('TeslaScanningServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TeslaScanningServiceService = TestBed.get(TeslaScanningServiceService);
    expect(service).toBeTruthy();
  });
});
