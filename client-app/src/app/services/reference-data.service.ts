import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../environment';
import {Observable} from 'rxjs';
import {IReferenceData} from '../models/reference-data.model';

@Injectable({
  providedIn: 'root',
})
export class ReferenceDataService {
  constructor(private httpClient: HttpClient) {}
  baseUrl = `${environment.webApiUrl}/ReferenceData`;

  getPriorities(): Observable<IReferenceData[]> {
    return this.httpClient.get<IReferenceData[]>(`${this.baseUrl}/Priorities`);
  }

  getStatuses(): Observable<IReferenceData[]> {
    return this.httpClient.get<IReferenceData[]>(`${this.baseUrl}/Statuses`);
  }
}
