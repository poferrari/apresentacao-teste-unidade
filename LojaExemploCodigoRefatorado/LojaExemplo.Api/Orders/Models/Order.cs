using LojaExemplo.Api.Models;
using System;
using System.Collections.Generic;

namespace Store.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal Total { get; set; }

        public IList<OrderProduct> OrderProducts { get; set; }
    }
}