import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Favorite } from 'src/app/models/Favorite';
import { TeslaClip } from 'src/app/models/TeslaClip';
import { TeslaFolder } from 'src/app/models/TeslaFolder';

@Injectable({
  providedIn: 'root'
})
export class FavoritesService {

  private readonly apiEndpoint = 'api/Favorites';
  constructor(private httpClient: HttpClient) { }

  getFavoriteFolders(): Observable<TeslaFolder[]> {
    return this.httpClient.get<TeslaFolder[]>(`${this.apiEndpoint}/folders`);
  }

  toggleFavoriteFolder(favorite: Favorite): Observable<void> {
    return this.httpClient.post<void>(`${this.apiEndpoint}/folders`, favorite);
  }

  getFavoriteClips(): Observable<TeslaClip[]> {
    return this.httpClient.get<TeslaClip[]>(`${this.apiEndpoint}/clips`);
  }

  toggleFavoriteClips(favorite: Favorite): Observable<void> {
    return this.httpClient.post<void>(`${this.apiEndpoint}/clips`, favorite);
  }


}
