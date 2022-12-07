using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Prism.Mvvm;

namespace ToDoList;

public class MainModel : BindableBase
{
    private ObservableCollection<TaskModel> TaskList { get; set; } = new();
    public void AddTask(string task, DateTime createDate)
    {
        TaskList.Add(new TaskModel(task, createDate));
        SaveData();
        RaisePropertyChanged("SortedTaskList");
    }

    public void AddTask(string task)
    {
        TaskList.Add(new TaskModel(task));
        SaveData();
        RaisePropertyChanged("SortedTaskList");
    }

    private void RemoveTask(TaskModel task)
    {
        TaskList.Remove(task);
    }

    public Dictionary<string, List<TaskModel>> SortTask()
    {
        var taskDictionary = new Dictionary<string, List<TaskModel>>
        {
            ["Overdue"] = new(),
            ["Without date"] = new(),
            ["Complete"] = new()
        };
        var deletedTasks = new List<TaskModel>();
        foreach (var task in TaskList)
        {
            if (task.IsDelete)
                deletedTasks.Add(task);
            else if (task.IsDone)
                taskDictionary["Complete"].Add(task);
            else if (task.DeadLine == default)
                taskDictionary["Without date"].Add(task);
            else if (task.DeadLine < DateTime.Today)
                taskDictionary["Overdue"].Add(task);
            else if (taskDictionary.ContainsKey(task.DeadLine.ToShortDateString()))
                taskDictionary[task.DeadLine.ToShortDateString()].Add(task);
            else
                taskDictionary[task.DeadLine.ToShortDateString()] = new List<TaskModel> { task };

        }

        foreach (var task in deletedTasks) RemoveTask(task);
        
        RaisePropertyChanged("SortedTaskList");
        return taskDictionary;
    }
    
    private void SaveData()
    {
        var xmlSerializer = new XmlSerializer(typeof(ObservableCollection<TaskModel>));

        using var fs = new FileStream("tasks.xml", FileMode.Create);
        xmlSerializer.Serialize(fs, TaskList);
    }

    private void LoadData()
    {
        var xmlSerializer = new XmlSerializer(typeof(ObservableCollection<TaskModel>));
        
        using var fs = new FileStream("tasks.xml", FileMode.OpenOrCreate);
        TaskList = (xmlSerializer.Deserialize(fs) as ObservableCollection<TaskModel>)!;
    }

    public MainModel()
    {
        LoadData();
    }

}
