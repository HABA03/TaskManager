import { removeTaskItemUseCase } from "../../application/useCases/taskItemUseCases/removeTaskItemUseCase";
import { taskItemRepository } from "../../infrastructure/repository/taskItemRepository";
import Button from "./button";
import '../../styles/taskCard.css';
import { useState } from "react";
import Modal from "./modal";

type cardProps = {
    id: number,
    name: string,
    description: string,
    status: string,
    createdDate: Date,
    refresh: () => void
};

function TaskCard({id, name, description, status, createdDate, refresh}: cardProps){

    const [openModal, setOpenModal] = useState<boolean>(false);

    const RemoveTask = async() =>{
        const repository = new taskItemRepository();
        await removeTaskItemUseCase(repository, id);
        refresh();
    }

    const formatDate = (date: any) => {
        if (!date) return "N/A";
        try {
            const dateObj = new Date(date);
            return dateObj.toLocaleDateString('es-ES', { 
                year: 'numeric', 
                month: '2-digit', 
                day: '2-digit',
                hour: '2-digit',
                minute: '2-digit'
            });
        } catch {
            return "N/A";
        }
    }

    return(
        <>
            <div className="taskcard-container">
                <div className="taskcard-information">
                    <p className="card-info-name">{name}</p>
                    <p className="card-info-description">{description}</p>
                    <div className="card-aditional-info">
                        {
                            status === "active" ? (
                                <p className="card-active-status" style={{backgroundColor: "#10b981"}}>Active</p>
                            ) : (
                                <p className="card-pending-status" style={{backgroundColor: "#ffb020"}}>Pending</p>
                            )
                        }

                        <p className="card-date">{formatDate(createdDate)}</p>
                    </div>
                </div>
                <div className="taskcard-buttons">
                    <Button color="#fff" background="#0b99ff" text="Update" onClick={() => setOpenModal(true)} icon={<i className="bi bi-pencil"></i>} />
                    <Button color="#fff" background="#ee3030" text="Delete" onClick={() => RemoveTask()} icon={<i className="bi bi-trash"></i>} />
                </div>
            </div>
            <Modal updateModal={true} title="Update Task" isOpen={openModal} closeModal={()=>setOpenModal(false)} id={id} />
        </>
    );
}

export default TaskCard;