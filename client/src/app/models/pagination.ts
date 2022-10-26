export interface MetaData {
    currentPage: number;
    totalPages: number;
    pageSize: number;
    totalCount: number;
}
                            //T generic type
export class PaginatedResponse<T> {
    items: T; //T here is arrays of products
    metaData: MetaData;

    constructor(items: T, metaData: MetaData) {
        this.items = items;
        this.metaData=metaData;
    }
}