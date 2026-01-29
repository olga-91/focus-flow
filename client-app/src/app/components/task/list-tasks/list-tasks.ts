import {Component, OnInit} from '@angular/core';
import {FormsModule, NgForm} from '@angular/forms';
import {RouterLink} from '@angular/router';
import {IFlowTask} from '../../../models/flow-task.model';
import {IReferenceData} from '../../../models/reference-data.model';
import {TaskService} from '../../../services/task.service';
import {ReferenceDataService} from '../../../services/reference-data.service';
import {firstValueFrom} from 'rxjs';
import {DatePipe, NgClass} from '@angular/common';
import {ToastService} from '../../../services/toast.service';

@Component({
  selector: 'app-list-tasks',
  imports: [
    FormsModule,
    RouterLink,
    NgClass,
    DatePipe
  ],
  templateUrl: './list-tasks.html',
  styleUrl: './list-tasks.css',
})
export class ListTasks implements OnInit {
  filter={
    project:'',
    statusId:null,
    priorityId:null
  }

  tasks: IFlowTask[] | undefined;
  priorities: IReferenceData[] | undefined;
  statuses: IReferenceData[] | undefined;

  constructor(
    private taskService: TaskService,
    private referenceDataService: ReferenceDataService,
    private toastService: ToastService) {}

  async ngOnInit(): Promise<void> {
    this.tasks = await firstValueFrom(this.taskService.filterTasks(this.filter));
    this.priorities = await firstValueFrom(this.referenceDataService.getPriorities());
    this.statuses = await firstValueFrom(this.referenceDataService.getStatuses());
  }

  async onDelete(id: number): Promise<void> {
    await firstValueFrom(this.taskService.deleteTask(id));
    this.toastService.success();
    this.tasks = await firstValueFrom(this.taskService.filterTasks(this.filter));
  }

  async onSubmit(form: NgForm) {
    this.tasks = await firstValueFrom(this.taskService.filterTasks(form.value));
  }
}
