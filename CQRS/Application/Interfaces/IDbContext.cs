using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
   public interface IDbContext
    {
        DbSet<Domain.Entities.User> Users { get; }

        Task SaveChangesAsync(CancellationToken cancellationToken, object obj = null);
    }
}
