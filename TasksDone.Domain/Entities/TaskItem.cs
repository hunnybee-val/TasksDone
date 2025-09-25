using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksDone.Domain.Enums;

namespace TasksDone.Domain.Entities
{
    /// <summary>
    /// Base class for tasks.
    /// </summary>
    public class TaskItem
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public TaskProgressStatus Status { get; private set; }
        public Guid ProjectId { get; private set; }

        public TaskItem(string title, Guid projectId)
        {
            Id = Guid.NewGuid();
            Title = title ?? throw new ArgumentNullException(nameof(title));
            ProjectId = projectId;
            Status = TaskProgressStatus.New;
        }

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
    }

}
