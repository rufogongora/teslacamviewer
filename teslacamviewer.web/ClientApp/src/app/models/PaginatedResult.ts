export interface PaginatedResult<T> { 
    data: Array<T>;
    totalCount: number;
    pageNumber: number;
    pageSize: number;
    totalPages: number;
}