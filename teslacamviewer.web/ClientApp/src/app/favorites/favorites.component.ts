import { Component, OnInit } from '@angular/core';
import { Favorite } from '../models/Favorite';
import { FavoritesService } from '../services/favorites/favorites.service';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.css']
})
export class FavoritesComponent implements OnInit {

  teslaFolders = [];
  search = '';
  constructor(
    private favoriteService: FavoritesService) { }

  ngOnInit() {
    this.favoriteService.getFavoriteFolders()
    .subscribe(res => {
      this.teslaFolders = res;
    });
  }

  toggleFavorite(name: string, type:string) {
    this.favoriteService.toggleFavoriteFolder({ name: name, type: type } as Favorite)
    .subscribe(() => {
      if (type === 'Folder') {
        const folderToUnfav = this.teslaFolders.find(f => f.name === name);
        this.teslaFolders.splice(this.teslaFolders.indexOf(folderToUnfav), 1);
        //weird hack for the search filter??
        this.teslaFolders = JSON.parse(JSON.stringify(this.teslaFolders));
      }
    });
  }

}
