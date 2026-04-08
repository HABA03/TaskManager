
export type TaskStatus = "active" | "pending" | "deleted"

export interface TaskItem {
    id: number;
    name: string;
    description: string;
    createdDate: Date;
    updatedDate: Date;
    status: TaskStatus;
}