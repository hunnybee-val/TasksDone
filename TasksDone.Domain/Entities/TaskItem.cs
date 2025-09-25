using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksDone.Domain.Enums;
using ReactiveUI;

namespace TasksDone.Domain.Entities
{
    /// <summary>
    /// Base class for tasks.
    /// </summary>
    public class TaskItem : ReactiveObject
    {
        #region Fields

        private string _title;
        private TaskProgressStatus _status;

        #endregion Fields

        #region Public Properties

        public Guid Id { get; private set; }
        public string Title
        {
            get => _title;
            private set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        public TaskProgressStatus Status
        {
            get => _status;
            private set => this.RaiseAndSetIfChanged(ref _status, value);
        }
        public Guid ProjectId { get; private set; }

        #endregion Public Properties

        #region Public Constructors

        public TaskItem() { } //EF Consctructor
        public TaskItem(string title, Guid projectId)
        {
            Id = Guid.NewGuid();
            Title = title ?? throw new ArgumentNullException(nameof(title));
            ProjectId = projectId;
            Status = TaskProgressStatus.New;
        }

        #endregion Public Constructors

        #region Methods

        public void Start()
        {
            if (Status != TaskProgressStatus.New)
                throw new InvalidOperationException("Task can only be started from 'New'.");
            Status = TaskProgressStatus.InProgress;
        }

        public void Complete()
        {
            if (Status != TaskProgressStatus.InProgress)
                throw new InvalidOperationException("Task must be InProgress to complete.");
            Status = TaskProgressStatus.Done;
        }

        #endregion Methods
    }

}
