using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.MongoDB;

namespace Application.DTOs.Responses
{
    public class ShoppingCart
    {
        public string? _id { get; set; }
        public List<ProductInCart>? ProductsInCart { get; set; }
        public double PriceTotal { get; set; }
        public string? Status { get; set; }
    }
}
