import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {firstValueFrom, Observable} from 'rxjs';
import {environment} from '../environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl: string = `${environment.webApiUrl}/auth`;

  constructor(private httpClient: HttpClient) {
  }

  async isAuthenticatedUser(): Promise<boolean> {
    const user = await firstValueFrom(this.loggedInUser());
    return user != null;
  }

  logout = () => {
    localStorage.removeItem('token');
  };

  loggedInUser(): Observable<{ username: string }> {
    return this.httpClient
      .get<{ username: string }>(`${this.baseUrl}/user`)
  };

  async loginUser(user: any): Promise<void> {
    const response = this.httpClient
      .post<{ token: string }>(`${this.baseUrl}/login`, user);

    try {
      const authResponse = await firstValueFrom(response);
      localStorage.setItem('token', authResponse.token);
    } catch (e) {
      console.error(e);
    }
  };
}
