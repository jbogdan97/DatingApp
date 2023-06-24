import { HttpClient, HttpParams, HttpResponse } from "@angular/common/http";
import { PaginatedResult } from "../_models/pagination";
import { Observable, map } from "rxjs";

export function getPaginatedResult<T>(url, params, http: HttpClient) {
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();
     return http.get<T>(url, { observe: 'response', params }).pipe(
       map((response: HttpResponse<T>) => {
         paginatedResult.result = response.body;
         if (response.headers.get('Pagination') !== null) {
           paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
         }
         return paginatedResult;
       }));
   }
 
   export function getPaginationHeaders(pageNumber: number, pageSize: number){
     let params = new HttpParams();
 
   params = params.append('pageNumber', pageNumber.toString());
   params = params.append('pageSize', pageSize.toString());
 
   return params;
   }