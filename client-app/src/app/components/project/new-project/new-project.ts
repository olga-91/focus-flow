import { Component } from '@angular/core';
import {FormsModule, NgForm, ReactiveFormsModule} from '@angular/forms';
import {firstValueFrom} from 'rxjs';
import {ProjectService} from '../../../services/project.service';
import {Router} from '@angular/router';
import {ToastService} from '../../../services/toast.service';

@Component({
  selector: 'app-new-project',
  imports: [
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './new-project.html',
  styleUrl: './new-project.css',
})
export class NewProject {
  project = {
    name: '',
    description: '',
  }

  constructor(
    private projectService: ProjectService,
    private router: Router,
    private toastService: ToastService
  ) {
  }

  async onSubmit(form: NgForm): Promise<void> {
    if (form.invalid) {
      return;
    }

    await firstValueFrom(this.projectService.addProject(form.value));
    this.toastService.success();
    await this.router.navigate(['/list-projects']);
  }
}
