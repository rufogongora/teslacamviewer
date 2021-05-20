import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TeslaFolder } from '../models/TeslaFolder';

@Injectable({
  providedIn: 'root'
})
export class TeslaFolderService {

  private readonly apiEndpoint = "api/teslaFolder";

  constructor(private http: HttpClient) { }

  getTeslaFolders(): Observable<TeslaFolder[]> {
    return this.http.get<TeslaFolder[]>(`${this.apiEndpoint}`);
  }

  getTeslaFolder(folderName: string): Observable<TeslaFolder> {
    return this.http.get<TeslaFolder>(`${this.apiEndpoint}/${folderName}`);
  }
}