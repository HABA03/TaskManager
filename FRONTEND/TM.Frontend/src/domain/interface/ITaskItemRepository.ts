import type { CreateTaskInput } from '../../application/task/createTaskInput';
import type { UpdateTaskInput } from '../../application/task/updateTaskInput';
import type { TaskItem } from '../entity/TaskItem';

export interface ITaskItemRepository {
    createTaskItem(taskItem:CreateTaskInput) : Promise<boolean>;  
    updateTaskItem(taskItem:UpdateTaskInput) : Promise<boolean>;
    removeTaskitem(id:number) : Promise<void>;
    getTaskItemById(id:number) : Promise<TaskItem>;
    getAllTaskItem() : Promise<TaskItem[]>;
}