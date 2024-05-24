using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime.SharedInterfaces;
using Application.DTOs;

namespace Application.Tests.Application.Tests.DTOs
{
    public class ShoppingCartHelperModel
    {
        public static ShoppingCart GetShoppingCartForCreation()
        {
            return new ShoppingCart()
            {
                ProductsInCart = new List<ProductInCart>()
                {
                    new ProductInCart
                    {
                        _id = "661805457b1da8ba4cb52995",
                        QuantityInCart = 4
                    },
                    new ProductInCart {
                        _id = "66185655155f38e1afb9fd29",
                        QuantityInCart = 3
                    },
                },
                PriceTotal = 0,
                Active = true
            };
        }

        public static ShoppingCart GetShoppingCartExistOnMongo()
        {
            return new ShoppingCart()
            {
                _id = "",
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
                CreatedAt = DateTime.Now,
                Active = true
            };
        }

        public static ShoppingCart GetShoppingCartForRemoveProducts() => new ShoppingCart()
        {
            _id = "663d35876527581c0636263e",
            ProductsInCart = new List<ProductInCart>()
            {
                new ProductInCart {_id = "661805457b1da8ba4cb52995"}
            },
        };
    }
}
