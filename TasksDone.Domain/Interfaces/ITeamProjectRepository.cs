using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TasksDone.Domain.Entities;

namespace TasksDone.Domain.Interfaces;

public interface ITeamProjectRepository
{
    /// <summary>
    /// Adds new project.
    /// </summary>
    Task AddProject(TeamProject project, CancellationToken cancellationToken = default);
    /// <summary>
    /// Adds new task to a project.
    /// </summary>
    Task AddTask(Guid projectId, TaskItem task, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a project by ID.
    /// </summary>
    Task<TeamProject?> GetProjectById(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all projects.
    /// </summary>
    Task<List<TeamProject>> GetAllProjects(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets projects with unfinished tasks.
    /// </summary>
    Task<List<TeamProject>> GetActiveProjects(CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates project's name or description.
    /// </summary>
    Task UpdateProject(TeamProject project, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a project  .
    /// </summary>
    Task DeleteProject(Guid id, CancellationToken cancellationToken = default);
}
