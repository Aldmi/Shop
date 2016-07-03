using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using Domain.Entities;

namespace WebUI.ViewModel
{
    public class OrderViewModel
    {
        public int Number { get; set; }          //OrderId
        public Address DeliveryAddress { get; set; }

        public List<Line> OrderItems  { get; set; } = new List<Line>();

        public decimal TotalPrice { get; set; }

        //TODO: add Data in order



        public class Line
        {
            public Product Product { get; set; }
            public int Quantity { get; set; }
        }
    }
}