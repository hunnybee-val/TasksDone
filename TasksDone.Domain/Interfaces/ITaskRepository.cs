using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TasksDone.Domain.Entities;

namespace TasksDone.Domain.Interfaces;

public interface ITaskRepository
{
    /// <summary>
    /// Adds new task to base.
    /// </summary>
    Task AddAsync(TaskItem task, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a task by ID.
    /// </summary>
    Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all tasks.
    /// </summary>
    Task<List<TaskItem>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all completed tasks.
    /// </summary>
    Task<List<TaskItem>> GetCompletedAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates task status.
    /// </summary>
    Task UpdateAsync(TaskItem task, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes task.
    /// </summary>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
