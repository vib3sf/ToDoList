using System;
using System.Collections.Generic;

namespace ToDoList;

public class MainModel
{
    private DateTime _currentDate = DateTime.Today;

    public Dictionary<string, TaskModel> TasksDictionary = new();

    private void CheckTasks()
    {
        foreach (var task in TasksDictionary.Values)
        {
            
        }
    }
    
    
    public MainModel()
    {
        
    }

}