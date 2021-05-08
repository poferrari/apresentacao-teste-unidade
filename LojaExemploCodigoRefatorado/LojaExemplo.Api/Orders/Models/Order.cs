using LojaExemplo.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Models
{
    public class Order
    {
        public Order(Customer customer, decimal deliveryFee, decimal discount, IList<OrderProduct> orderProducts)
        {
            CustomerId = customer.Id;
            Code = Guid.NewGuid().ToString().ToUpper().Substring(0, 8);
            Date = DateTime.Now;
            DeliveryFee = deliveryFee;
            OrderProducts = orderProducts;

            var subTotal = orderProducts.Sum(t => t.Total);
            SubTotal = subTotal;
            Discount = discount;

            Total = subTotal - discount + deliveryFee;
        }

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