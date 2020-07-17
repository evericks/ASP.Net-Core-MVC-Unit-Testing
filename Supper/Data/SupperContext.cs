using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class SupperContext : DbContext
    {
        public SupperContext(DbContextOptions<SupperContext> options)
            : base(options)
        {
        }

        public DbSet<Data.Entities.Player> Player { get; set; }

        public DbSet<Data.Entities.Comment> Comment { get; set; }

        public DbSet<Data.Entities.User> User { get; set; }
    }
}
