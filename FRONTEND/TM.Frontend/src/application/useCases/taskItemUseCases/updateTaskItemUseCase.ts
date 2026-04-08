import type { ITaskItemRepository } from "../../../domain/interface/ITaskItemRepository";
import type { UpdateTaskInput } from "../../task/updateTaskInput";


export const updateTaskItemUseCase = async (repository:ITaskItemRepository, updateTaskInput:UpdateTaskInput) : Promise<void> => {
    if(updateTaskInput.id <= 0)
        throw new Error("Id must be greater than 0");

    if(!updateTaskInput.name)
        throw new Error("Name cant be null");

    if(!updateTaskInput.description)
        throw new Error("Description cant be null");

    const response = await repository.updateTaskItem(updateTaskInput);

    if(!response)
        throw new Error("Error updating information");
}