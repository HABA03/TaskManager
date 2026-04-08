import type { CreateTaskInput } from "../../application/task/createTaskInput";
import type { UpdateTaskInput } from "../../application/task/updateTaskInput";
import type { TaskItem, TaskStatus } from "../../domain/entity/TaskItem";
import type { CreateTaskItemRequest } from "../dtos/TaskItem/CreateTaskItemRequest";
import type { UpdateTaskItemRequest } from "../dtos/TaskItem/UpdateTaskItemRequest";


export const mapTaskToCreateTaskRequest = (taskItem:CreateTaskInput) : CreateTaskItemRequest => ({
    name: taskItem.name,
    description: taskItem.description
});

export const mapTaskToUpdateTaskRequest = (taskItem:UpdateTaskInput) : UpdateTaskItemRequest => ({
    id: taskItem.id,
    name: taskItem.name,
    description: taskItem.description
});

export const mapGetTaskToTaskItem = (getTaskItem:any) : TaskItem => {
    const mapped: TaskItem = {
        id: getTaskItem.id ?? getTaskItem.Id ?? 0,
        name: getTaskItem.name ?? getTaskItem.Name ?? "",
        description: getTaskItem.description ?? getTaskItem.Description ?? "",
        createdDate: getTaskItem.createdDate 
            ? new Date(getTaskItem.createdDate) 
            : (getTaskItem.CreatedDate ? new Date(getTaskItem.CreatedDate) : new Date()),
        updatedDate: getTaskItem.updatedDate 
            ? new Date(getTaskItem.updatedDate) 
            : (getTaskItem.UpdatedDate ? new Date(getTaskItem.UpdatedDate) : new Date()),
        status: mapStatusToTaskStatus(getTaskItem.status ?? getTaskItem.Status),
    };
    return mapped;
};

const mapStatusToTaskStatus = (status:number | string) : TaskStatus => {
    // Handle both number and string status values
    const statusNum = typeof status === 'string' 
        ? statusStringToNumber(status) 
        : status;
    
    switch(statusNum){
        case 0:
            return "active";
        case 1:
            return "pending";
        case 2:
            return "deleted";
        default:
            console.warn("Unknown status:", status, "defaulting to active");
            return "active";
    }
};

const statusStringToNumber = (statusStr: string): number => {
    const normalized = statusStr.toLowerCase();
    if (normalized === "active") return 0;
    if (normalized === "pending") return 1;
    if (normalized === "deleted") return 2;
    return 0; // default to active
};