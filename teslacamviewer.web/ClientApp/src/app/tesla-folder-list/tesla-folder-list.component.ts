import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { TeslaFolder } from '../models/TeslaFolder';
import { TeslaFolderService } from '../services/tesla-folder.service';
import { catchError } from 'rxjs/operators';
import { ConfirmationModalComponent } from '../shared/confirmation-modal/confirmation-modal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FavoritesService } from '../services/favorites/favorites.service';
import { Favorite } from '../models/Favorite';
import { TeslaDataService } from '../services/tesla-data-service/tesla-data.service';
import { TeslaData } from '../models/TeslaData';
import { TeslaScanningServiceService } from '../services/tesla-scanning-service/tesla-scanning-service.service';

@Component({
  selector: 'app-tesla-folder-list',
  templateUrl: './tesla-folder-list.component.html',
  styleUrls: ['./tesla-folder-list.component.css']
})
export class TeslaFolderListComponent implements OnInit {

  teslaFolders$: Observable<TeslaFolder[]>;
  error:string;
  search = "";
  orderByProperty = "name";
  teslaData: TeslaData;
  rescanning = false;

  constructor(
    private teslaFolderService: TeslaFolderService,
    private modalService: NgbModal,
    private teslaDataService: TeslaDataService,
    private teslaScanningService: TeslaScanningServiceService,
    private favoriteService: FavoritesService) { }

  ngOnInit() {
    this.getTeslaData();
    this.teslaDataService.loadTeslaData();
  }

  getTeslaData() {
    const subscription = this.teslaDataService.teslaData$.subscribe(res => {
      this.teslaData = res;
      const lastRun = new Date(this.teslaData.lastRun);
      // if last run is more than 1 minute ago, rescan
      if (lastRun.getTime() < (new Date().getTime() - 5 * 60 * 1000)) { 
        this.rescanning = true;
        this.startRescan();
      } else {
        this.getFolders();
      }
      
      subscription.unsubscribe();
    });
  }

  startRescan() {
    this.teslaScanningService.startRescan()
      .subscribe(() => {
        this.getTeslaData();
        this.teslaDataService.loadTeslaData();
      }, (err) => {
        console.error(err);
      }, () => {
        this.rescanning = false;
      });
  }

  changeOrderBy(property: string) {
    if (this.isOrderedByProperty(property)) {
      if (!this.isReversedOrdered()) {
        this.orderByProperty = `-${property}`;
        return;
      }
    } 
    this.orderByProperty = property
  }

  isOrderedByProperty(property: string) {
    return this.orderByProperty.indexOf(property) !== -1;
  }

  isReversedOrdered() {
    return this.orderByProperty.indexOf("-") !== -1;
  }

  delete(tf: TeslaFolder) {
    const modalRef = this.modalService.open(ConfirmationModalComponent);
    modalRef.componentInstance.title = 'Confirm Deletion';
    modalRef.componentInstance.text = `Are you sure you want to delete the folder ${tf.name} for the date ${tf.teslaEvent.timeStamp}.
       Please notice, deletion is PERMANENT.`;
    modalRef.result.then((res) => {
      if (res) {
            this.teslaFolderService.deleteTeslaFolder(tf)
            .subscribe(() => {
              this.getFolders();
            });
      }
    }).catch(() => {

    });
  }

  getColorIfFavorite(tf : TeslaFolder) {
    return tf.favorite ? 'red' : 'black';
  }

  toggleFavorite(tf: TeslaFolder) {
    const newFav = { name: tf.name,  type: 'Folder'} as Favorite
    this.favoriteService.toggleFavoriteFolder(newFav)
    .subscribe(() => {
      tf.favorite = !tf.favorite;
    });
  }

  convertDirectoryNameToDate(directoryName: string) {    
    return new Date(`${directoryName.split("_")[0]}T00:00:00`);
  }

  private getFolders() {
    this.teslaFolders$ = this.teslaFolderService.getTeslaFolders()
    .pipe(catchError((err) => {
      this.error = err.error;
      return of;
    }));
  }

}
