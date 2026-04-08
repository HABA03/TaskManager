import { useEffect, useState } from 'react';
import '../../styles/modal.css'
import Button from './button';
import type { CreateTaskInput } from '../../application/task/createTaskInput';
import { createTaskItemUseCase } from '../../application/useCases/taskItemUseCases/createTaskItemUseCase';
import { taskItemRepository } from '../../infrastructure/repository/taskItemRepository';
import type { UpdateTaskInput } from '../../application/task/updateTaskInput';
import { updateTaskItemUseCase } from '../../application/useCases/taskItemUseCases/updateTaskItemUseCase';
import { getTaskItemByIdUseCase } from '../../application/useCases/taskItemUseCases/getTaskItemByIdUseCase';

type modalProps = {
    createModal?: boolean,
    updateModal?: boolean,
    isOpen: boolean,
    closeModal: ()=>void,
    title: string, 
    id?: number
}

type taskFormState = {
    id: number,
    name: string,
    description: string
}

function Modal({createModal, updateModal, isOpen, closeModal, title, id=0}: modalProps){

    const [formData, setFormData] = useState<taskFormState>({id: 0, name: "", description: ""});
    const [error, setError] = useState<string>("");
    const [loading, setLoading] = useState<boolean>(false);
    const repository = new taskItemRepository();

    const FillData = async() =>{
        try {
            setError("");
            const response = await getTaskItemByIdUseCase(repository, id);
            setFormData({id: response.id ?? 0, name: response.name ?? "", description: response.description ?? ""})
        } catch (err: any) {
            console.error("Error filling form data:", err);
            setError(err?.message || "Failed to load task data");
        }
    } 

    useEffect(()=>{
        if(updateModal && id && id > 0)
            FillData();
    }, [updateModal, id]);

    const CreateTask = async() => {
        try {
            setError("");
            setLoading(true);
            const dto: CreateTaskInput = { name: formData.name, description: formData.description }
            await createTaskItemUseCase(repository, dto);
            closeModal();
        } catch (err: any) {
            console.error("Error creating task:", err);
            setError(err?.message || "Failed to create task");
        } finally {
            setLoading(false);
        }
    };

    const UpdateTask = async() => {
        try {
            setError("");
            setLoading(true);
            const dto: UpdateTaskInput = { id: formData.id, name: formData.name, description: formData.description };
            await updateTaskItemUseCase(repository, dto);
            closeModal();
        } catch (err: any) {
            console.error("Error updating task:", err);
            setError(err?.message || "Failed to update task");
        } finally {
            setLoading(false);
        }
    };

    if(!isOpen) return null;

    return(
        <>
            <div className="modal-layer">
            </div>

            <div className="modal-content">
                <h2 className='modal-h2'>{title}</h2>
                {error && <div style={{color: "red", marginBottom: "10px", fontSize: "12px"}}>{error}</div>}
                {updateModal && <input type='number' readOnly value={formData.id} onChange={(e) => setFormData({...formData, id: Number.parseInt(e.target.value)})} className='modal-input' id='id' placeholder='Id' /> }
                <input disabled={loading} value={formData.name} onChange={(e) => setFormData({...formData, name: e.target.value})} className='modal-input' id='name' type="text" placeholder='Name' />
                <input disabled={loading} value={formData.description} onChange={(e) => setFormData({...formData, description: e.target.value})} className='modal-input' id='description' type="text" placeholder='Description' />
                <div className='modal-buttons-container'>
                    <Button text='Close' color='#fff' icon={<i className="bi bi-x-circle"></i>} background='#3334' onClick={()=>closeModal()} />
                    {createModal && <Button text={loading ? 'Creating...' : 'Create'} color='#fff' icon={<i className="bi bi-check-square"></i>} background='#06b6a4' onClick={()=>CreateTask()} />}
                    {updateModal && <Button text={loading ? 'Updating...' : 'Update'} color='#fff' icon={<i className="bi bi-check-square"></i>} background='#0b99ff' onClick={()=>UpdateTask()} />}
                </div>
            </div>
        </>
    );
}

export default Modal;