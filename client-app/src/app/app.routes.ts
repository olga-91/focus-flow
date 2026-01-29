import {Routes} from '@angular/router';
import {RegisterUser} from './components/register-user/register-user';
import {Dashboard} from './components/dashboard/dashboard';
import {UserLogin} from './components/user-login/user-login';
import {NewProject} from './components/project/new-project/new-project';
import {NewTask} from './components/task/new-task/new-task';
import {EditTask} from './components/task/edit-task/edit-task';
import {ListProjects} from './components/project/list-projects/list-projects';
import {EditProject} from './components/project/edit-project/edit-project';
import {ListTasks} from './components/task/list-tasks/list-tasks';
import {AuthGuard} from './guards/auth-guard';

export const routes: Routes = [{
  path: '',
  redirectTo: 'dashboard',
  pathMatch: 'full'
  },
  {
    path: 'dashboard',
    component: Dashboard
  },
  {
    path: 'register-user',
    component: RegisterUser
  },
  {
    path: 'login',
    component: UserLogin
  },
  {
    path: 'new-project',
    component: NewProject, canActivate: [AuthGuard]
  },
  {
    path: 'list-projects',
    component: ListProjects, canActivate: [AuthGuard],
  },
  {
    path: 'list-tasks',
    component: ListTasks, canActivate: [AuthGuard]
  },
  {
    path: 'new-task',
    component: NewTask, canActivate: [AuthGuard]
  },
  {
    path: 'task/:id',
    component: EditTask, canActivate: [AuthGuard]
  },
  {
    path: 'project/:id',
    component: EditProject, canActivate: [AuthGuard]
  }
];
