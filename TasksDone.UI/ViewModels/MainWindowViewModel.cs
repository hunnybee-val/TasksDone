using System;
using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using TasksDone.Domain.Entities;
using TasksDone.Infrastructure.Repositories;
using TasksDone.Infrastructure.Persistance;

namespace TasksDone.UI.ViewModels;

public class MainWindowViewModel : ReactiveObject
{
    //Projects
    #region Private Fields

    private string _newProjectName = string.Empty;
    private TeamProject? _selectedProject;

    #endregion Private Fields

    #region Public Properties
    public string NewProjectName
    {
        get => _newProjectName;
        set => this.RaiseAndSetIfChanged(ref _newProjectName, value);
    }

    public ObservableCollection<TeamProject> Projects { get; } = new();
    private readonly TeamProjectRepository _repo = new(new AppDbContext());

    public TeamProject? SelectedProject
    {
        get => _selectedProject;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedProject, value);
            RefreshTasks();
        }
    }

    public ReactiveCommand<Unit, Unit> AddProjectCommand { get; }

    #endregion Public Properties

    //Tasks
    #region Private Fields

    private string _newTaskTitle = string.Empty;

    #endregion Private Fields

    #region Public Properties

    public string NewTaskTitle
    {
        get => _newTaskTitle;
        set => this.RaiseAndSetIfChanged(ref _newTaskTitle, value);
    }

    public ObservableCollection<TaskItem> Tasks { get; } = new();

    public ReactiveCommand<Unit, Unit> AddTaskCommand { get; }

    #endregion Public Properties


    #region Public Constructors

    public MainWindowViewModel()
    {
        AddProjectCommand = ReactiveCommand.Create(AddProject);
        AddTaskCommand = ReactiveCommand.Create(AddTask);
    }

    #endregion Public Constructors

    #region Methods

    private async void AddProject()
    {
        if (!string.IsNullOrWhiteSpace(NewProjectName))
        {
            var project = new TeamProject(NewProjectName, Guid.NewGuid());
            await _repo.AddProject(project);
            Projects.Add(project);
            SelectedProject = project;
            NewProjectName = string.Empty;
        }
    }

    private void AddTask()
    {
        if (SelectedProject == null || string.IsNullOrWhiteSpace(NewTaskTitle)) return;

        var task = new TaskItem(NewTaskTitle, new System.Guid());
        SelectedProject.AddTask(task);

        Tasks.Add(task); // синхронно добавляем в UI
        NewTaskTitle = string.Empty;
    }

    private void RefreshTasks()
    {
        Tasks.Clear();
        if (SelectedProject != null)
        {
            foreach (var t in SelectedProject.Tasks)
                Tasks.Add(t);
        }
    }

    #endregion Methods
}
