import type { TaskItem } from "../../../domain/entity/TaskItem";
import type { ITaskItemRepository } from "../../../domain/interface/ITaskItemRepository";


export const getAllTaskItemUseCase = async (repository: ITaskItemRepository) : Promise<TaskItem[]> => {
    const response = await repository.getAllTaskItem();

    if(!response)
        throw new Error("Error getting the information");

    return response;
} 