import { HttpStatusCode } from "axios";
import type { TaskItem } from "../../domain/entity/TaskItem";
import type { ITaskItemRepository } from "../../domain/interface/ITaskItemRepository";
import { httpClient } from "../api/httpClient";
import { mapGetTaskToTaskItem, mapTaskToCreateTaskRequest, mapTaskToUpdateTaskRequest } from '../mappers/taskItemMapper';
import type { CreateTaskInput } from "../../application/task/createTaskInput";
import type { UpdateTaskInput } from "../../application/task/updateTaskInput";


export class taskItemRepository implements ITaskItemRepository{

    async createTaskItem(taskItem: CreateTaskInput): Promise<boolean> {
        const dto = mapTaskToCreateTaskRequest(taskItem);
        const response = await httpClient.post("TaskItem/Create", dto);
        return response.status == HttpStatusCode.Ok ? true : false; 
    }

    async updateTaskItem(taskItem: UpdateTaskInput): Promise<boolean> {
        const dto = mapTaskToUpdateTaskRequest(taskItem);
        const response = await httpClient.put("TaskItem/Update", dto);
        return response.status == HttpStatusCode.Ok ? true : false;
    }

    async removeTaskitem(id: number): Promise<void> {
        await httpClient.put("TaskItem/Remove", {Id: id});
    }

    async getTaskItemById(id: number): Promise<TaskItem> {
        const response =  await httpClient.put("TaskItem/GetById", {Id: id})
        const task = mapGetTaskToTaskItem(response.data);
        return task;
    }

    async getAllTaskItem(): Promise<TaskItem[]> {
        const response = await httpClient.get("TaskItem/GetAll");
        
        if (!response.data || !Array.isArray(response.data)) {
            return [];
        }

        const tasks: TaskItem[] = response.data.map((item: any) => {
            return mapGetTaskToTaskItem(item);
        });
        
        return tasks;
    }

}