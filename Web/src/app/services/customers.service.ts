import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CustomerResponse } from '../models/CustomerResponse';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class CustomersService {
  constructor(private http: HttpClient) {
  }

  getCustomers(page: number, recordCount: number): Observable<CustomerResponse[]> {
    return this.http.get<CustomerResponse[]>(`${environment.APIUrl}/Customers/GetCustomers?pageNumber=${page}&numberOfRecords=${recordCount}`)
  }
}
