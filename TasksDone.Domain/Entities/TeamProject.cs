using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TasksDone.Domain.Enums;

namespace TasksDone.Domain.Entities
{
    /// <summary>
    /// Project class for tasks with multiple assignees.
    /// </summary>
    public class TeamProject
    {
        public Guid Id { get; private set; }
        public string ProjectName { get; private set; }
        /// <summary>
        /// Tasks list is only available for Administrator <c>User</c>s
        /// </summary>
        public bool IsPrivateUseOnly { get; private set; }
        public int TasksCount { get; set; }
        public string? Description { get; private set; }

        private readonly List<TaskItem> _tasks = new();
        public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();

        public TeamProject(string projectTitle, Guid projectId, bool isPrivate = false, string description = "")
        {
            Id = Guid.NewGuid();
            ProjectName = projectTitle ?? throw new ArgumentNullException(nameof(projectTitle));
            IsPrivateUseOnly = isPrivate;
            TasksCount = 0;
            Description = description;
        }

        public void AddTask(TaskItem task)
        {
            if (task == null) throw new ArgumentNullException(nameof(task));
            _tasks.Add(task);
        }

        public void ChangeName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName)) throw new ArgumentException("Name cannot be empty");
            ProjectName = newName;
        }

        public void ChangeDescription(string newDescription)
        {
            Description = newDescription ?? string.Empty;
        }
    }
}
