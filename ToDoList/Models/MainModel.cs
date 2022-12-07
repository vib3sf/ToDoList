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
    public ObservableCollection<TaskModel> TaskList { get; set; } = new();
    public Dictionary<string, List<TaskModel>> SortedTaskList { get; private set; }

    public void AddTask(string task, DateTime createDate)
    {
        TaskList.Add(new TaskModel(task, createDate));
        SortedTaskList = SortTask();
        SaveData();
        RaisePropertyChanged("SortedTaskList");
    }

    public void AddTask(string task)
    {
        TaskList.Add(new TaskModel(task));
        SortedTaskList = SortTask();
        SaveData();
        RaisePropertyChanged("SortedTaskList");
    }

    public void RemoveTask(TaskModel task)
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
        SortedTaskList = SortTask();
    }

}
