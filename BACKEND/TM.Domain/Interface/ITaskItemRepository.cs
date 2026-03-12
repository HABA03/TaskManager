using TM.Domain.Entity;

namespace TM.Domain.Interface;

public interface ITaskItemRepository
{
    Task<bool> CreateTaskItem(TaskItem taskItem, CancellationToken cancellationToken);
    Task<bool> UpdateTaskItem(TaskItem taskItem, CancellationToken cancellationToken);
    Task<bool> RemoveTaskItem(int id, CancellationToken cancellationToken);
    Task<TaskItem> GetTaskItemById(int id, CancellationToken cancellationToken);
    Task<List<TaskItem>> GetAllTaskItem(CancellationToken cancellationToken);
}