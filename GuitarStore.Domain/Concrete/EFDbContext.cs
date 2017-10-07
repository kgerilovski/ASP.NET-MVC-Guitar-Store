using GuitarStore.Domain.Entities;
<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
=======
using System.Data.Entity;
>>>>>>> 1a40977b42b1850b739beb5d58867f68993f82e6

namespace GuitarStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
<<<<<<< HEAD
=======
        public DbSet<User> Users { get; set; }
>>>>>>> 1a40977b42b1850b739beb5d58867f68993f82e6
    }
}
