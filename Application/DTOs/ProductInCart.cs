using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProductInCart
    {
        public string? _id { get; set; }
        public string? Name { get; set; }
        public double UnitPrice { get; set; }
        public int QuantityInCart { get; set; }
    }

    public class IdOnly
    {
        public string? _id { get; set; }
    }

    public class GetIds
    {
        public List<IdOnly> _ids { get; set; } = new List<IdOnly>();
    }
}
