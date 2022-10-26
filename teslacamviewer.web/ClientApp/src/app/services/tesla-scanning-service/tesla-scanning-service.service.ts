import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TeslaScanningServiceService {

  private readonly apiEndpoint = "api/teslaScanner";  

  constructor(private http: HttpClient) { 
  }

  startRescan() {
    return this.http.post(`${this.apiEndpoint}`, {});
  }
}
