import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../environment';
import {IUser} from '../models/user.model';
import {firstValueFrom, Observable} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private httpClient: HttpClient) {
  }

  baseUrl = `${environment.webApiUrl}/User`;

  registerUser(user: IUser): Observable<IUser> {
    return this.httpClient.post<IUser>(`${this.baseUrl}/register`, user);
  }

  getAllUsers():Observable<IUser[]> {
    return this.httpClient.get<IUser[]>(`${this.baseUrl}/all`)
  }
}
