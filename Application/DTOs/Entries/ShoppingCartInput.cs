using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Responses;

namespace Application.DTOs.Entries
{
    public class ShoppingCartInput
    {
        public List<ProductInCart>? ProductsInCart { get; set; }
    }
}
