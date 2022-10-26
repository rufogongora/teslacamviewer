import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { TeslaData } from 'src/app/models/TeslaData';

@Injectable({
  providedIn: 'root'
})
export class TeslaDataService {

  private readonly apiEndpoint = "api/teslaData";
  teslaData$ = new Subject<TeslaData>();

  constructor(private http: HttpClient) { 
    this.loadTeslaData();
  }

  getTeslaData(): Observable<TeslaData> {
    return this.http.get<TeslaData>(`${this.apiEndpoint}`);
  }

  loadTeslaData() {
    this.getTeslaData().subscribe(td => this.teslaData$.next(td));
  }
}
