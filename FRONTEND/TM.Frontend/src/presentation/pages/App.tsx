import { useEffect, useState } from "react";
import type { TaskItem } from "../../domain/entity/TaskItem";
import { taskItemRepository } from "../../infrastructure/repository/taskItemRepository";
import { getAllTaskItemUseCase } from "../../application/useCases/taskItemUseCases/getAllTaskItemUseCase";
import Button from "../components/button";
import '../../styles/App.css';
import TaskCard from "../components/taskCard";
import Modal from "../components/modal";

function App(){
    const [tasks, setTasks] = useState<TaskItem[]>([]);
    const [createModal, setCreateModal] = useState<boolean>(false);
    const [updateModal, setUpdateModal] = useState<boolean>(false);
    const [error, setError] = useState<string>("");

    const getTasks = async () => {
        try {
            setError("");
            const repository = new taskItemRepository();
            const response = await getAllTaskItemUseCase(repository);
            setTasks(response);
        } catch (err: any) {
            setError(err?.message || "Failed to load tasks");
        }
    }

    useEffect(() => {
        getTasks();
    }, []);
    

    return(
        <>
            <div className="app-content-container">
                <div className="app-part1-content">
                    <div className="app-part1-info">
                        <h1 className="app-part1-h1">Tasks</h1>
                        <p className="app-part1-p">Manage your projects and daily responsibilities.</p>
                    </div>
                    <Button text="Create New Task" onClick={() => setCreateModal(true)} color="#fff" background="#06b6a4" icon={<i className="bi bi-plus"></i>} />
                </div>
            </div>
            
            {error && <div style={{color: "red", padding: "10px", margin: "10px"}}>{error}</div>}
            
            <div className="app-cards-container">
                {tasks.length === 0 && !error && (
                    <div style={{padding: "20px", color: "#999", textAlign: "center"}}>
                        No tasks found. Create a new task to get started!
                    </div>
                )}
                {tasks.map((task, index) => (
                    <TaskCard key={task.id || index} id={task.id} name={task.name} description={task.description} status={task.status} createdDate={task.createdDate} refresh={() => getTasks()} />
                ))}
            </div>

            <Modal isOpen={createModal} closeModal={()=>setCreateModal(false)} title="Create Task" createModal={true} />
        </>
    );
}

export default App;