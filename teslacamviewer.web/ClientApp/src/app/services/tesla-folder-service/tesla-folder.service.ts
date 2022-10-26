import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaginatedResult } from 'src/app/models/PaginatedResult';
import { TeslaFolder } from 'src/app/models/TeslaFolder';
import { TeslaFolderWithoutClips } from 'src/app/models/TeslaFolderWithoutClips';
import { FolderColumnEnum } from './columnEnum';

@Injectable({
  providedIn: 'root'
})
export class TeslaFolderService {

  private readonly apiEndpoint = "api/teslafolderv2";

  private _numberOfFoldersPerPage = 10;
  get numberOfFoldersPerPage() { return this._numberOfFoldersPerPage; }
  set numberOfFoldersPerPage(value: number) { this._numberOfFoldersPerPage = value; }

  constructor(
    private http: HttpClient) { }

  getTeslaFolders(pageNumber: number, orderBy: FolderColumnEnum, search = ""): Observable<PaginatedResult<TeslaFolderWithoutClips>> {
    
    const params =  search ? { params : { search: search } } : undefined;
    return this.http.get<PaginatedResult<TeslaFolderWithoutClips>>(
      `${this.apiEndpoint}/paginate/${pageNumber}/${this.numberOfFoldersPerPage}/${orderBy}`,
      params);
  }

  getTeslaFolder(folderName: string, folderType: string): Observable<TeslaFolder> {
    return this.http.get<TeslaFolder>(`${this.apiEndpoint}/${folderType}/${folderName}`);
  }

  deleteTeslaFolder(folder: TeslaFolder) {
    return this.http.delete(`${this.apiEndpoint}/${folder.folderType}/${folder.name}`);
  }
}
