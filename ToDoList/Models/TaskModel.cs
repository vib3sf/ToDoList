using System;

namespace ToDoList;

public class TaskModel
{
    public string Task { get; set; }
    public bool IsDone { get; set; }

    public DateTime DeadLine { get; set; }

    public TaskModel(string task)
    {
        Task = task;
        IsDone = false;
    }
    
    public TaskModel(string task, DateTime deadLine) : this(task)
    {
        DeadLine = deadLine;
    }
    
}