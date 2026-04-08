import type { TaskItem } from "../../../domain/entity/TaskItem";
import type { ITaskItemRepository } from "../../../domain/interface/ITaskItemRepository";


export const getTaskItemByIdUseCase = async (repository: ITaskItemRepository, id:number) : Promise<TaskItem> => {

    if(id <= 0)
        throw new Error("Id must be greater than 0");

    console.log("Recibimos el ID: " + id);

    const response = await repository.getTaskItemById(id);

    console.log("Recibimos esta data: " + {response});

    if(!response)
        throw new Error("Error getting the information");

    return response;
}