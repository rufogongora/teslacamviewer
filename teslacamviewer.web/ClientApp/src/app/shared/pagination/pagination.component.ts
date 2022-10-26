import { Component, Input, OnInit } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnInit {

  @Input() currentPage: number; // current page
  @Input() totalPages: number;
  @Input() pageChanged$: Subject<number>; // Observable to emit page changes
  constructor() { }

  ngOnInit() {
  }
  
  changePage(page: number) {
    this.pageChanged$.next(page);
  }

}
