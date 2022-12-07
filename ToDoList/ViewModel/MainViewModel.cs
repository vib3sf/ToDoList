using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace ToDoList.ViewModel;

public class MainViewModel : BindableBase
{
    private MainModel _mainModel = new();
    public Dictionary<string, List<TaskModel>> SortedTaskDictionary => _mainModel.SortTask();
    public DelegateCommand CreateCommand { get; }
    
    public string TaskTextBlock { get; set; }
    public DateTime? CreateTaskDate { get; set; }
    
    public TaskModel SelectedTask { get; set; }
    
    public DelegateCommand RefreshCommand { get; set; }

    public static string CurrentDate => $"Current date is {DateTime.Today.ToShortDateString()}";

    public MainViewModel()
    {
        _mainModel.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };

        CreateCommand = new DelegateCommand(() =>
        {

            if (CreateTaskDate == null)
                _mainModel.AddTask(TaskTextBlock!);
            else
                _mainModel.AddTask(TaskTextBlock!, (DateTime)CreateTaskDate!);
            
            RaisePropertyChanged("SortedTaskDictionary");
        });
        RefreshCommand = new DelegateCommand(() =>
        {
            _mainModel.Refresh();
            RaisePropertyChanged("SortedTaskDictionary");
        });
    }
}