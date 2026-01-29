import { Component } from '@angular/core';
import {FormsModule, NgForm, ReactiveFormsModule} from '@angular/forms';
import {UserService} from '../../services/user.service';
import {firstValueFrom} from 'rxjs';
import {Router} from '@angular/router';
import {ToastService} from '../../services/toast.service';

@Component({
  selector: 'app-register-user',
  imports: [
    ReactiveFormsModule,
    FormsModule
  ],
  templateUrl: './register-user.html',
  styleUrl: './register-user.css',
})
export class RegisterUser {
  constructor(private userService: UserService,
              private router: Router,
              private toastService: ToastService) {
  }

  user = {
    username: '',
    email: '',
    password: '',
    name: ''
  };

  async onSubmit(form: NgForm): Promise<void> {
    if (form.invalid) return;

    await firstValueFrom(this.userService.registerUser(form.value));
    this.toastService.success();
    this.router.navigate(['/']);
  }
}
