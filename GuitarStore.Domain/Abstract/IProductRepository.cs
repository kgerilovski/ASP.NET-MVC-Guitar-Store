using GuitarStore.Domain.Entities;
using System.Collections.Generic;

namespace GuitarStore.Domain.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }

        void SaveProduct(Product product);
        Product DeleteProduct(int productId);
    }
}
