import type { ITaskItemRepository } from "../../../domain/interface/ITaskItemRepository";
import type { CreateTaskInput } from "../../task/createTaskInput";


export const createTaskItemUseCase = async (repository: ITaskItemRepository,createTaskInput:CreateTaskInput) : Promise<void> =>{

    if(!createTaskInput.name || !createTaskInput.description)
        throw new Error("Name and description cant be null")

    const result = await repository.createTaskItem(createTaskInput);

    if(!result)
        throw new Error("Error creating new task item");
}