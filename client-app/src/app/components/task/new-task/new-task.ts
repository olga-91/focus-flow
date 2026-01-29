import {Component, OnInit} from '@angular/core';
import {FormsModule, NgForm} from "@angular/forms";
import {firstValueFrom} from 'rxjs';
import {TaskService} from '../../../services/task.service';
import {ReferenceDataService} from '../../../services/reference-data.service';
import {Router} from '@angular/router';
import {IReferenceData} from '../../../models/reference-data.model';
import {UserService} from '../../../services/user.service';
import {IUser} from '../../../models/user.model';
import {ToastService} from '../../../services/toast.service';

@Component({
  selector: 'app-new-task',
    imports: [
        FormsModule
    ],
  templateUrl: './new-task.html',
  styleUrl: './new-task.css',
})
export class NewTask implements OnInit {
  flowTask = {
    title: '',
    description: '',
    dueDate: null,
    priorityId: null,
    statusId: null,
    assignedUserId: null,
  }

  priorities: IReferenceData[] | undefined;
  statuses:IReferenceData[] | undefined;
  users: IUser[] | undefined;

  constructor(
    private referenceDataService: ReferenceDataService,
    private userService: UserService,
    private taskService: TaskService,
    private router: Router,
    private toastService: ToastService
    ) {}

  async ngOnInit(): Promise<void> {
       this.priorities = await firstValueFrom(this.referenceDataService.getPriorities());
       this.statuses = await firstValueFrom(this.referenceDataService.getStatuses());
       this.users = await firstValueFrom(this.userService.getAllUsers());
  }

  async onSubmit(form: NgForm): Promise<void> {
    if (form.invalid) {
      return;
    }

    await firstValueFrom(this.taskService.addTask(form.value));
    this.toastService.success();
    await this.router.navigate(['/']);
  }
}
