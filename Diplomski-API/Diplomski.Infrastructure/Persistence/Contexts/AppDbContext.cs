using Diplomski.Core.Entities;
using Diplomski.Infrastructure.Persistence.DataSeed;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Diplomski.Infrastructure.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        private readonly DbContextOptions<AppDbContext> _options;

        public DbSet<File> Files { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<FileType> FileTypes { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            this._options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.Seed();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow;
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = now;
                }
                ((BaseEntity)entity.Entity).UpdatedAt = now;
            }
        }

        private async void AddFileTypes()
        { 
            using (var context = new AppDbContext(_options))
            {
                string [] types = { "image/", "text/", "application/" };
                var fileTypes =  await context.FileTypes.Select(fileType => fileType.Type).ToListAsync();

                foreach (var type in types)
                {
                    if (!fileTypes.Contains(type))
                    {
                        await context.FileTypes.AddAsync(new FileType { Type = type });
                    }
                }
            }
        }
    }
}
