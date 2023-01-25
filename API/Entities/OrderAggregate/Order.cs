using System;
using System.Collections.Generic;

namespace API.Entities.OrderAggregate
{
    public class Order
    {
        public int Id { get; set; }

        public string BuyerId { get; set; }

        public ShippingAddress ShippingAddress { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public List<OrderItem> OrderItems { get; set; }

        public long Subtotal { get; set; }
        public long DeliveryFee { get; set; }

        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        
        //nje metod qe na kthen totalin e shumes, bashkangjitur me pagesen e postes
        public long GetTotal()
        {
            return Subtotal + DeliveryFee;
        }
    }
}