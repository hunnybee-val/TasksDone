using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TasksDone.Domain.Entities;
using TasksDone.Domain.Enums;
using TasksDone.Domain.Interfaces;
using TasksDone.Infrastructure.Persistance;

namespace TasksDone.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _db;

    public TaskRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(TaskItem task, CancellationToken cancellationToken = default)
    {
        await _db.Tasks.AddAsync(task, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _db.Tasks
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<List<TaskItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _db.Tasks
            .OrderBy(t => t.Title) 
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TaskItem>> GetCompletedAsync(CancellationToken cancellationToken = default)
    {
        return await _db.Tasks
            .Where(t => t.Status == TaskProgressStatus.Done) 
            .OrderByDescending(t => t.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(TaskItem task, CancellationToken cancellationToken = default)
    {
        _db.Tasks.Update(task);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var task = await _db.Tasks.FindAsync([id], cancellationToken);
        if (task != null)
        {
            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
