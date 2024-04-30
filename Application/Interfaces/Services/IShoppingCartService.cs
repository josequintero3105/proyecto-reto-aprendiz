using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Core.Entities.MongoDB;

namespace Application.Interfaces.Services
{
    public interface IShoppingCartService
    {
        public Task CreateShoppingCart(ShoppingCart shoppingCart);
    }
}
