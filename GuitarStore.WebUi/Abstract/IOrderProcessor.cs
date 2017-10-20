using GuitarStore.WebUi.Entities;

namespace GuitarStore.WebUi.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
