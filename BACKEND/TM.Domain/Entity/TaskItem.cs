using TM.Domain.Enum;

namespace TM.Domain.Entity;

public class TaskItem
{
    public int Id {get; set;}
    public string Name {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
    public DateTime CreatedDate {get; set;}
    public DateTime UpdatedDate {get; set;}
    public TaskStatusEnum Status {get; set;}
}