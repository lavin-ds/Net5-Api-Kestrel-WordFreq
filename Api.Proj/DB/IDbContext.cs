using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WordFreqApi.Models;

namespace WordFreqApi.DB
{
    public interface IDbContext:IDisposable
    {
        DbSet<Source> SourceItems { get; set; }
        DbSet<Submission> SubmissionItems { get; set; }
        DbSet<HighFrequencyWord> HighFrequencyWordItems { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        int SaveChanges();

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity: class;
    }
}