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
        public string? Id { get; set; }
        public List<ProductCollection> Products { get; set; }
        public double PriceTotal { get; set;}
        public DateTime CreatedAt { get; set; }
        public bool State {  get; set; }
        /// <summary>
        /// Exists
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool HasProducts(string productId) => Products.Exists(p => p._id == productId);
        /// <summary>
        /// Find Index
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public int FindProductPosition(string productId) => Products.FindIndex(p => p._id == productId);
    }
}
