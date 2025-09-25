using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksDone.Domain.Interfaces;
using TasksDone.Domain.Entities;
using TasksDone.Domain.Enums;
using TasksDone.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace TasksDone.Infrastructure.Repositories
{
    public class TeamProjectRepository : ITeamProjectRepository
    {

    private readonly AppDbContext _db;

        public TeamProjectRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(TeamProject project, CancellationToken cancellationToken = default)
        {
            await _db.Projects.AddAsync(project, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<TeamProject?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.Projects
                .Include(p => p.Tasks) 
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<List<TeamProject>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Projects
                .Include(p => p.Tasks)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<TeamProject>> GetActiveProjectsAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Projects
                .Include(p => p.Tasks)
                .Where(p => p.Tasks.Any(t => t.Status != TaskProgressStatus.Done))
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(TeamProject project, CancellationToken cancellationToken = default)
        {
            _db.Projects.Update(project);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
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
