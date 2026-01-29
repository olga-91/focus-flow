import {Component, OnInit} from '@angular/core';
import {FormsModule, NgForm} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {ProjectService} from '../../../services/project.service';
import {firstValueFrom} from 'rxjs';
import {IProject} from '../../../models/project.model';
import {ToastService} from '../../../services/toast.service';

@Component({
  selector: 'app-edit-project',
  imports: [
    FormsModule
  ],
  templateUrl: './edit-project.html',
  styleUrl: './edit-project.css',
})
export class EditProject implements OnInit {
  id: number | undefined;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private projectService: ProjectService,
    private toastService: ToastService
  ) {}

  project : IProject = {
    id: null,
    name: '',
    description: '',
  }
  async ngOnInit(): Promise<void> {
    this.id = this.activatedRoute.snapshot.params["id"];
    this.project = await firstValueFrom(this.projectService.getProject(this.id!));
  }

  async onSubmit(form: NgForm): Promise<void> {
    if (form.invalid) {
      return;
    }

    await firstValueFrom(this.projectService.updateProject(this.id!, form.value));
    this.toastService.success();
    await this.router.navigate(['/list-projects']);
  }
}
