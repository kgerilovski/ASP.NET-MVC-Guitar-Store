using GuitarStore.WebUi.Entities;
using System.Collections.Generic;

namespace GuitarStore.WebUi.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }

        void SaveProduct(Product product);
        Product DeleteProduct(int productId);
    }
}
