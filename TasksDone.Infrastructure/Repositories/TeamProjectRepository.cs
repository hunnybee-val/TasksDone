using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksDone.Domain.Interfaces;
using TasksDone.Domain.Entities;
using TasksDone.Domain.Enums;
using TasksDone.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using DynamicData;
using System.Reactive.Linq;
using DynamicData.Binding;

namespace TasksDone.Infrastructure.Repositories
{
    public class TeamProjectRepository : ITeamProjectRepository
    {

    private readonly AppDbContext _db;

        public TeamProjectRepository(AppDbContext db)
        {
            _db = db;
        }

        public List<TeamProject> GetAllProjects() => _db.Projects.ToList();

        public async Task AddProject(TeamProject project, CancellationToken cancellationToken = default)
        {
            await _db.Projects.AddAsync(project, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task AddTask(Guid projectId, TaskItem task, CancellationToken cancellationToken = default)
        {
            var project = await _db.Projects.Include(p => p.Tasks)
                                            .FirstOrDefaultAsync(p => p.Id == projectId);
            if (project != null)
            {
                project.AddTask(task);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<TeamProject?> GetProjectById(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.Projects
                .Include(p => p.Tasks) 
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<List<TeamProject>> GetAllProjects(CancellationToken cancellationToken = default)
        {
            return await _db.Projects
                .Include(p => p.Tasks)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<TeamProject>> GetActiveProjects(CancellationToken cancellationToken = default)
        {
            return await _db.Projects
                .Include(p => p.Tasks)
                .Where(p => p.Tasks.Any(t => t.Status != TaskProgressStatus.Done))
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateProject(TeamProject project, CancellationToken cancellationToken = default)
        {
            _db.Projects.Update(project);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteProject(Guid id, CancellationToken cancellationToken = default)
        {
            var project = await _db.Projects.FindAsync(new object[] { id }, cancellationToken);
            if (project != null)
            {
                _db.Projects.Remove(project);
                await _db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
