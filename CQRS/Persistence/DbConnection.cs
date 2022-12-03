using Application.Interfaces;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbConnection : DbContext , IDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbConnection(DbContextOptions<DbConnection> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken, object obj = null)
        {
          return base.SaveChangesAsync(cancellationToken);
        }
    }

}
