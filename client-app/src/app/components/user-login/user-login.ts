import {Component, OnInit} from '@angular/core';
import {firstValueFrom} from 'rxjs';
import {FormsModule} from '@angular/forms';
import {Router} from '@angular/router';
import {AuthService} from '../../services/auth.service';

@Component({
  selector: 'app-user-login',
  imports: [
    FormsModule
  ],
  templateUrl: './user-login.html',
  styleUrl: './user-login.css',
})
export class UserLogin implements OnInit {
  userCredentials= {
    username: '',
    password: ''
  };
  user: { username : string } | undefined;

  constructor(private authService: AuthService,
              private router: Router) {
  }

  async ngOnInit() {
    this.user = await firstValueFrom(this.authService.loggedInUser());
  }

  async login(){
    await this.authService.loginUser(this.userCredentials);
    location.reload();
  }

  logout(){
    this.authService.logout();
    this.router.navigate(['/']);
  }
}
