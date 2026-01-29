import {Component, OnInit} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {RouterLink} from '@angular/router';
import {ProjectService} from '../../../services/project.service';
import {IProject} from '../../../models/project.model';
import {firstValueFrom} from 'rxjs';
import {ToastService} from '../../../services/toast.service';

@Component({
  selector: 'app-list-projects',
  imports: [
    FormsModule,
    RouterLink
  ],
  templateUrl: './list-projects.html',
  styleUrl: './list-projects.css',
})
export class ListProjects implements OnInit {
  projects: IProject[] | undefined;

  constructor(private projectService: ProjectService,
              private toastService: ToastService) {}

  async ngOnInit(): Promise<void> {
    this.projects = await firstValueFrom(this.projectService.getProjects());
  }

  async onDelete(id: number): Promise<void> {
    await firstValueFrom(this.projectService.deleteProject(id));
    this.toastService.success();
    this.projects = await firstValueFrom(this.projectService.getProjects());
  }
}
