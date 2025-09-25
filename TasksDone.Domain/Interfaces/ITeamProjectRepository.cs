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
    Task AddAsync(TeamProject project, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a project by ID.
    /// </summary>
    Task<TeamProject?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all projects.
    /// </summary>
    Task<List<TeamProject>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets projects with unfinished tasks.
    /// </summary>
    Task<List<TeamProject>> GetActiveProjectsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates project's name or description.
    /// </summary>
    Task UpdateAsync(TeamProject project, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a project  .
    /// </summary>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
