using Microsoft.EntityFrameworkCore;
using WordFreqApi.Models;

namespace WordFreqApi.DB
{
    public class AppContext : DbContext, IDbContext
    {
        public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
        }

        public DbSet<Submission> SubmissionItems { get; set; }
        public DbSet<Source> SourceItems { get; set; }
        public DbSet<HighFrequencyWord> HighFrequencyWordItems { get; set; }

    }
}