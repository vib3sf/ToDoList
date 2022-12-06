using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDoList;

public class MainModel
{

    public List<TaskModel> TaskList = new();

    public void AddTask(string task, int year, int month, int day)
    {
        TaskList.Add(new TaskModel(task, new DateTime(year, month, day)));
    }

    public void AddTask(string task)
    {
        TaskList.Add(new TaskModel(task));
    }

    public void RemoveTask(string taskDate, TaskModel task)
    {
        TaskList.Remove(task);
    }

    public Dictionary<string, List<TaskModel>> SortTask()
    {
        var taskDictionary = new Dictionary<string, List<TaskModel>>
        {
            ["Overdue"] = new(),
            ["Without date"] = new()
        };
        foreach (var task in TaskList)
        {
            if (task.DeadLine == default)
                taskDictionary["Without date"].Add(task);
            else if (task.DeadLine < DateTime.Today)
                taskDictionary["Overdue"].Add(task);
            else if (taskDictionary.ContainsKey(task.DeadLine.ToShortDateString()))
                taskDictionary[task.DeadLine.ToShortDateString()].Add(task);
            else
                taskDictionary[task.DeadLine.ToShortDateString()] = new List<TaskModel> { task };

        }

        return taskDictionary;
    }

    public MainModel()
    {
        TaskList.Add(new TaskModel("почесать жопу", DateTime.Today));
        TaskList.Add(new TaskModel("почесать жопу", DateTime.Today));
        TaskList.Add(new TaskModel("почесать жопу", new DateTime(2005, 3, 4)));
        TaskList.Add(new TaskModel("покакать"));
        TaskList.Add(new TaskModel("почесать жопу", DateTime.Today));
        TaskList.Add(new TaskModel("почесать жопу", DateTime.Today));
        TaskList.Add(new TaskModel("почесать жопу", new DateTime(2005, 3, 4)));
        TaskList.Add(new TaskModel("покакать"));
        TaskList.Add(new TaskModel("почесать жопу", DateTime.Today));
        TaskList.Add(new TaskModel("почесать жопу", DateTime.Today));
        TaskList.Add(new TaskModel("почесать жопу", new DateTime(2005, 3, 4)));
        TaskList.Add(new TaskModel("покакать"));

    }

}