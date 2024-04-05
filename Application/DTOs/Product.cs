using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.PlantillaEntitys;

namespace Application.DTOs
{
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool State { get; set; }
        public Entity? Data { get; set; }
        
    }
}
