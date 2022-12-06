using System.Collections.Generic;

namespace ToDoList.ViewModel;

public class MainViewModel
{
    private MainModel _mainModel = new();
    public Dictionary<string, List<TaskModel>> SortedTaskDictionary => _mainModel.SortTask();
}