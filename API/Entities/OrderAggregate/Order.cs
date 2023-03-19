using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Entities.OrderAggregate
{
    public class Order
    {
        public int Id { get; set; }

        public string BuyerId { get; set; }

        [Required]
        public ShippingAddress ShippingAddress { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow; //changed it beacuse postgreeSQL does not store as datetime.now

        public List<OrderItem> OrderItems { get; set; }

        public long Subtotal { get; set; }
        public long DeliveryFee { get; set; }

        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        public string PaymentIntentId { get; set; }
        
        //nje metod qe na kthen totalin e shumes, bashkangjitur me pagesen e postes
        public long GetTotal()
        {
            return Subtotal + DeliveryFee;
        }
    }
}