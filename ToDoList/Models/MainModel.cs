using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Mvvm;

namespace ToDoList;

public class MainModel : BindableBase
{
    private readonly ObservableCollection<TaskModel> _taskList = new();
    public Dictionary<string, List<TaskModel>> SortedTaskList { get; private set; }

    public void AddTask(string task, DateTime createDate)
    {
        _taskList.Add(new TaskModel(task, createDate));
        SortedTaskList = SortTask();
        RaisePropertyChanged("SortedTaskList");
    }

    public void AddTask(string task)
    {
        _taskList.Add(new TaskModel(task));
        SortedTaskList = SortTask();
        RaisePropertyChanged("SortedTaskList");
    }

    public void RemoveTask(TaskModel task)
    {
        _taskList.Remove(task);
    }

    public Dictionary<string, List<TaskModel>> SortTask()
    {
        var taskDictionary = new Dictionary<string, List<TaskModel>>
        {
            ["Overdue"] = new(),
            ["Without date"] = new()
        };
        foreach (var task in _taskList)
        {
            if (task.IsDone)
                continue;
            
            if (task.DeadLine == default)
                taskDictionary["Without date"].Add(task);
            else if (task.DeadLine < DateTime.Today)
                taskDictionary["Overdue"].Add(task);
            else if (taskDictionary.ContainsKey(task.DeadLine.ToShortDateString()))
                taskDictionary[task.DeadLine.ToShortDateString()].Add(task);
            else
                taskDictionary[task.DeadLine.ToShortDateString()] = new List<TaskModel> { task };

        }
        RaisePropertyChanged("SortedTaskList");
        return taskDictionary;
    }

    public MainModel()
    {
        _taskList.Add(new TaskModel("почесать жопу", DateTime.Today));
        _taskList.Add(new TaskModel("почесать жопу", DateTime.Today));
        _taskList.Add(new TaskModel("почесать жопу", new DateTime(2225, 3, 4)));
        SortedTaskList = SortTask();
    }

}
