import {Component, OnInit} from '@angular/core';
import {TaskService} from '../../services/task.service';
import {firstValueFrom} from 'rxjs';
import {FormsModule, } from '@angular/forms';
import {IStatistics} from '../../models/statistics.model';

@Component({
  selector: 'app-dashboard',
  imports: [
    FormsModule
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit {
  statistics: IStatistics[] | undefined;

  constructor(
    private taskService: TaskService) {}

  async ngOnInit(): Promise<void> {
    this.statistics = await firstValueFrom(this.taskService.getStatistics());
  }
}
