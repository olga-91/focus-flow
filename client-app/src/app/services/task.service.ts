import { Injectable } from '@angular/core';
import {IProject} from '../models/project.model';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {environment} from '../environment';
import {IFlowTask} from '../models/flow-task.model';
import {IFilter} from '../models/filter.model';
import {IStatistics} from '../models/statistics.model';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  constructor(private httpClient: HttpClient) {}
  baseUrl = `${environment.webApiUrl}/FlowTask`;

  addTask(flowTask:IFlowTask): Observable<IFlowTask> {
    return this.httpClient.post<IFlowTask>(`${this.baseUrl}`, flowTask);
  }

  updateTask(id:number, flowTask:IFlowTask): Observable<IFlowTask> {
    return this.httpClient.put<IFlowTask>(`${this.baseUrl}/${id}`, flowTask);
  }

  getTask(id:number){
    return this.httpClient.get<IFlowTask>(`${this.baseUrl}/${id}`);
  }

  filterTasks(filter:IFilter): Observable<IFlowTask[]> {
    return this.httpClient.post<IFlowTask[]>(`${this.baseUrl}/filter`, filter);
  }

  getStatistics(): Observable<IStatistics[]> {
    return this.httpClient.get<IStatistics[]>(`${this.baseUrl}/statistics`);
  }

  deleteTask(id?:number){
    return this.httpClient.delete<IFlowTask>(`${this.baseUrl}/${id}`);
  }
}
