import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../environment';
import {Observable} from 'rxjs';
import {IProject} from '../models/project.model';

@Injectable({
  providedIn: 'root',
})
export class ProjectService {
  constructor(private httpClient: HttpClient) {}
  baseUrl = `${environment.webApiUrl}/Project`;

  addProject(project: IProject): Observable<IProject> {
    return this.httpClient.post<IProject>(`${this.baseUrl}`, project);
  }

  updateProject(id:number, project: IProject): Observable<boolean> {
    return this.httpClient.put<boolean>(`${this.baseUrl}/${id}`, project);
  }

  getProject(id:number): Observable<IProject> {
    return this.httpClient.get<IProject>(`${this.baseUrl}/${id}`);
  }

  getProjects(): Observable<IProject[]> {
    return this.httpClient.get<IProject[]>(`${this.baseUrl}/all`);
  }

  deleteProject(id:number): Observable<void> {
    return this.httpClient.delete<void>(`${this.baseUrl}/${id}`);
  }
}
