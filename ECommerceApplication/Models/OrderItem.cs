﻿namespace ECommerceApplication.Models
{
    public class OrderItem
    {
        public long OrderItemId { get; set; }

        public long OrderId { get; set; }

        public long ProductId { get; set; }

        public long Quantity { get; set; }
    }
}
