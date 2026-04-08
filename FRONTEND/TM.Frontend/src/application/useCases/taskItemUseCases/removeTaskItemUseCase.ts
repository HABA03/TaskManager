import type { ITaskItemRepository } from "../../../domain/interface/ITaskItemRepository";


export const removeTaskItemUseCase = async (repository: ITaskItemRepository, id:number) : Promise<void> => {
    if(id <= 0)
        throw new Error("Id must be greater than 0");

    try {
        await repository.removeTaskitem(id);
    } catch (error) {
        throw new Error("Error removing");
    }
}