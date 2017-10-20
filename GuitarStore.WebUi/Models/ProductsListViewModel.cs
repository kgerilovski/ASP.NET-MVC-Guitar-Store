using GuitarStore.WebUi.Entities;
using System.Collections.Generic;

namespace GuitarStore.WebUi.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}