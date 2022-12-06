using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDoList;

public class MainModel
{
    private DateTime _currentDate = DateTime.Today;

    public Dictionary<string, List<TaskModel>> TasksDictionary = new();

    private void CheckOverdueTasks()
    {
        TasksDictionary["overdue"] = new List<TaskModel>();
        foreach (var taskDate in TasksDictionary.Keys
                     .Where(taskDate => DateTime.Parse(taskDate) < _currentDate))
        {
            TasksDictionary["overdue"].AddRange(TasksDictionary[taskDate]);
            TasksDictionary.Remove(taskDate);
        }
    }

    public void AddTask(string task, int year, int month, int day)
    {
        var taskModel = new TaskModel(task, new DateTime(year, month, day));
        var strDate = taskModel.DeadLine.ToShortDateString();
        if (TasksDictionary.ContainsKey(strDate))
            TasksDictionary[strDate].Add(taskModel);
        else
            TasksDictionary[strDate] = new List<TaskModel> { taskModel };
    }

    public void AddTaskWithoutDate(string task)
    {
        TasksDictionary["Without date"].Add(new TaskModel(task));
    }

    public void RemoveTask(string taskDate, TaskModel task)
    {
        TasksDictionary[taskDate].Remove(task);
    }
    
    public MainModel()
    {
        CheckOverdueTasks();
    }

}