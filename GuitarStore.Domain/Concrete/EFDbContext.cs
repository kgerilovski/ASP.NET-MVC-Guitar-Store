using GuitarStore.Domain.Entities;
using System.Data.Entity;

namespace GuitarStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
