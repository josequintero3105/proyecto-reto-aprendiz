using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Entries
{
    public class ProductInput
    {
        public string? Name { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public bool State { get; set; }
    }
}
