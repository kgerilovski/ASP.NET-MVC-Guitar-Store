using GuitarStore.Domain.Abstract;
<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
=======
using System.Collections.Generic;
>>>>>>> 1a40977b42b1850b739beb5d58867f68993f82e6
using GuitarStore.Domain.Entities;

namespace GuitarStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private readonly EFDbContext context = new EFDbContext();
        public IEnumerable<Product> Products
        {
<<<<<<< HEAD
            get
            { return context.Products; }
=======
            get { return context.Products; }
>>>>>>> 1a40977b42b1850b739beb5d58867f68993f82e6
        }

        public Product DeleteProduct(int productId)
        {
            Product dbEntry = context.Products.Find(productId);
<<<<<<< HEAD
            if (dbEntry != null)
=======
            if(dbEntry != null)
>>>>>>> 1a40977b42b1850b739beb5d58867f68993f82e6
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveProduct(Product product)
        {
<<<<<<< HEAD
            if (product.ProductId == 0)
            {
                context.Products.Add(product);
            }
            else
=======
           if (product.ProductId == 0)
            {
                context.Products.Add(product);
            }
           else
>>>>>>> 1a40977b42b1850b739beb5d58867f68993f82e6
            {
                Product dbEntry = context.Products.Find(product.ProductId);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            context.SaveChanges();
        }
    }
}
