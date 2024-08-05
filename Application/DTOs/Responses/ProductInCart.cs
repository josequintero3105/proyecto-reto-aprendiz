using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses
{
    public class ProductInCart
    {
        public string? _id { get; set; }
        public string? Name { get; set; }
        public double UnitPrice { get; set; }
        public double QuantityInCart { get; set; }
    }
}
