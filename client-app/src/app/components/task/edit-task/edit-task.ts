import {Component, OnInit} from '@angular/core';
import {FormsModule, NgForm} from "@angular/forms";
import {ReferenceDataService} from '../../../services/reference-data.service';
import {TaskService} from '../../../services/task.service';
import {ActivatedRoute, Router} from '@angular/router';
import {firstValueFrom} from 'rxjs';
import {IReferenceData} from '../../../models/reference-data.model';
import {IFlowTask} from '../../../models/flow-task.model';
import {DatePipe} from '@angular/common';
import {IUser} from '../../../models/user.model';
import {UserService} from '../../../services/user.service';
import {ProjectService} from '../../../services/project.service';
import {IProject} from '../../../models/project.model';
import {ToastService} from '../../../services/toast.service';

@Component({
  selector: 'app-edit-task',
  imports: [
    FormsModule,
    DatePipe
  ],
  templateUrl: './edit-task.html',
  styleUrl: './edit-task.css',
})
export class EditTask implements OnInit {
  flowTask: IFlowTask = {
    id: null,
    title: '',
    description: '',
    dueDate: null,
    priorityId: null,
    statusId: null,
    projectId: null,
    assignedUserId: null
  };

  priorities: IReferenceData[] | undefined;
  statuses: IReferenceData[] | undefined;
  id: number | undefined;
  users: IUser[] | undefined;
  projects: IProject[] | undefined;

  constructor(
    private referenceDataService: ReferenceDataService,
    private taskService: TaskService,
    private userService: UserService,
    private projectService: ProjectService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private toastService: ToastService

  ) {}

  async ngOnInit(): Promise<void> {
    this.id = this.activatedRoute.snapshot.params["id"];
    this.flowTask = await firstValueFrom(this.taskService.getTask(this.id!));
    this.priorities = await firstValueFrom(this.referenceDataService.getPriorities());
    this.statuses = await firstValueFrom(this.referenceDataService.getStatuses());
    this.users = await firstValueFrom(this.userService.getAllUsers());
    this.projects = await firstValueFrom(this.projectService.getProjects());
  }

  async onSubmit(form: NgForm): Promise<void> {
    if (form.invalid) {
      return;
    }

    await firstValueFrom(this.taskService.updateTask(this.id!, form.value));
    this.toastService.success();
    await this.router.navigate(['/list-tasks']);
  }
}
