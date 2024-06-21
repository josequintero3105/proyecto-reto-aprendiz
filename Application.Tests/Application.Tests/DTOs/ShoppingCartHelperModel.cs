using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime.SharedInterfaces;
using Application.DTOs;
using Application.DTOs.Entries;

namespace Application.Tests.Application.Tests.DTOs
{
    public class ShoppingCartHelperModel
    {
        public static ShoppingCartInput GetShoppingCartForCreation()
        {
            return new ShoppingCartInput()
            {
                ProductsInCart = new List<ProductInCart>() {}
                
            };
        }

        public static ShoppingCart GetShoppingCartFromMongo() => new ShoppingCart()
        {
            _id = "664f40fed44e5362205a9381",
            ProductsInCart = new List<ProductInCart>()
            {
                new ProductInCart
                {
                    _id = "661805457b1da8ba4cb52995",
                    QuantityInCart = 4
                },
                new ProductInCart
                {
                    _id = "66185655155f38e1afb9fd29",
                    QuantityInCart = 3
                },
            },
            PriceTotal = 40000,
            Active = true
        };        

        public static ShoppingCart GetShoppingCartForRemoveProducts() => new ShoppingCart()
        {
            _id = "6644d077042e0563da8a5600",
            ProductsInCart = new List<ProductInCart>()
            {
                new ProductInCart {_id = "661805457b1da8ba4cb52995"}
            },
        };
    }
}
