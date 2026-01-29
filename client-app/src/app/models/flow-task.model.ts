export interface IFlowTask {
  id?: number| null
  title: string| null
  description?: string| null
  dueDate?: Date| null
  priorityId?: number| null
  priorityName?: string| null
  statusId?: number| null
  statusName?: string| null
  projectName?: string| null
  projectId?: number| null
  assignedUserId?: number| null
  assignedUser?: number| null
}
