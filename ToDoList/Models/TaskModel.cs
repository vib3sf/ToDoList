using System;

namespace ToDoList;

[Serializable]
public class TaskModel
{
    public string Task { get; set; }
    public bool IsDone { get; set; }

    public bool IsDelete { get; set; }
    public DateTime DeadLine { get; set; }
    
    public TaskModel() {}

    public TaskModel(string task)
    {
        Task = task;
        IsDone = false;
        IsDelete = false;
    }
    
    public TaskModel(string task, DateTime deadLine) : this(task)
    {
        DeadLine = deadLine;
    }
    
}