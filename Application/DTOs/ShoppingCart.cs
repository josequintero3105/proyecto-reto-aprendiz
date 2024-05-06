using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.MongoDB;

namespace Application.DTOs
{
    public class ShoppingCart
    {
        public string? _id { get; set; }
        public List<ProductInCart>? ProductsInCart { get; set; }
        public double PriceTotal { get; set;}
        public DateTime CreatedAt { get; set; }
        public bool State {  get; set; }
    }
}
