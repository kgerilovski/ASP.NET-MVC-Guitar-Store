using System.Collections.Generic;
using System.Linq;

namespace GuitarStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection.Where(p => p.Product.ProductId == product.ProductId)
<<<<<<< HEAD
                                          .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
=======
                .FirstOrDefault();
            if(line == null)
            {
                lineCollection.Add(
                    new CartLine { Product = product, Quantity = quantity });
>>>>>>> 1a40977b42b1850b739beb5d58867f68993f82e6
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(p => p.Product.ProductId == product.ProductId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(p => p.Product.Price * p.Quantity);
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
    }
}
