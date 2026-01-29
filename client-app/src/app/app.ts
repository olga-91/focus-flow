import {Component, OnInit} from '@angular/core';
import {RouterLink, RouterOutlet} from '@angular/router';
import {UserLogin} from './components/user-login/user-login';
import {AuthService} from './services/auth.service';
import {Toast} from './components/util/toast/toast';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink, UserLogin, Toast],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  isAuthenticated: boolean | undefined;

  constructor(protected authService: AuthService) {}

  async ngOnInit(): Promise<void> {
    this.isAuthenticated = await this.authService.isAuthenticatedUser()
    }
}
