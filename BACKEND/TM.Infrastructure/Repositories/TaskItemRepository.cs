using Microsoft.EntityFrameworkCore;
using TM.Domain.Entity;
using TM.Domain.Enum;
using TM.Domain.Interface;
using TM.Infrastructure.Context.TMContext;

namespace TM.Infrastructure.Repositories.TaskItemRepository;

public class TaskItemRepository : ITaskItemRepository
{
    private readonly TMContext _context;

    public TaskItemRepository(TMContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateTaskItem(TaskItem taskItem, CancellationToken cancellationToken)
    {
        bool response = false;
        if(taskItem != null)
        {
            taskItem.CreatedDate = DateTime.UtcNow;
            taskItem.UpdatedDate = DateTime.UtcNow;
            await _context.TaskItem.AddAsync(taskItem, cancellationToken);
            response = await _context.SaveChangesAsync(cancellationToken) > 0;   
        }
        return response;
    }

    public async Task<List<TaskItem>> GetAllTaskItem(CancellationToken cancellationToken)
    {
        return await _context.TaskItem.Where(t => t.Status != TaskStatusEnum.Deleted).ToListAsync(cancellationToken);
    }

    public async Task<TaskItem> GetTaskItemById(int id, CancellationToken cancellationToken)
    {
        var response = await _context.TaskItem.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        return response != null ? response : new TaskItem();
    }

    public async Task<bool> RemoveTaskItem(int id, CancellationToken cancellationToken)
    {
        bool response = false;
        if(id > 0)
        {
            var currentTask = await _context.TaskItem.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
            if(currentTask != null)
            {
                _context.TaskItem.Remove(currentTask);
                response = await _context.SaveChangesAsync(cancellationToken) > 0;
            }
        }
        return response;
    }

    public async Task<bool> UpdateTaskItem(TaskItem taskItem, CancellationToken cancellationToken)
    {
        bool response = false;
        if(taskItem != null)
        {
            taskItem.UpdatedDate = DateTime.UtcNow;
            _context.TaskItem.Update(taskItem);
            response = await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        return response;
    }
}