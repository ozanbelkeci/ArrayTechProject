using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Entities
{
    public class Products
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public DateTime AddedDate { get; set; }
        public int Quantity { get; set; }
    }
}
