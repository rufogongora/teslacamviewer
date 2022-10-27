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

  getPages() {
    const pages = [];
    const lowerBound = this.clamp(this.currentPage - 2, 1, this.totalPages);
    const upperBound = this.clamp(this.currentPage + 2, 1, this.totalPages);
    console.log(lowerBound, upperBound); // 1 5
    for(let i = lowerBound; i <= upperBound; i++) {
      pages.push(i);
    }
    return pages;
  }
  
  morePagesToTheRight(): boolean {
    return this.currentPage + 2 < this.totalPages;
  }

  morePagesToTheLeft(): boolean {
    return this.currentPage - 2 > 1;
  }

  movePagination(numberOfPages: number) {
    this.changePage(this.clamp(this.currentPage + numberOfPages, 1, this.totalPages)); // 3 5
  }

  clamp = (num: number, min: number, max: number) => Math.min(Math.max(num, min), max);

}
