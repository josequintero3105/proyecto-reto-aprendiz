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
using Core.Entities.MongoDB;
using MongoDB.Driver;

namespace Application.Tests.Application.Tests.DTOs
{
    public class ShoppingCartHelperModel
    {
        public static ShoppingCartInput GetShoppingCartForCreation()
        {
            return new ShoppingCartInput()
            {
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
                }
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
        };

        public static ShoppingCartCollection GetShoppingCartCollectionFromMongo() => new ShoppingCartCollection()
        {
            _id = "664f40fed44e5362205a9381",
            ProductsInCart = new List<ProductInCartCollection>()
            {
                new ProductInCartCollection
                {
                    _id = "661805457b1da8ba4cb52995",
                    QuantityInCart = 4
                },
                new ProductInCartCollection
                {
                    _id = "66185655155f38e1afb9fd29",
                    QuantityInCart = 3
                },
            },
            PriceTotal = 40000,
        };

        public static ShoppingCart GetShoppingCartForRemoveProducts() => new ShoppingCart()
        {
            _id = "6644d077042e0563da8a5600",
            ProductsInCart = new List<ProductInCart>()
            {
                new ProductInCart {_id = "661805457b1da8ba4cb52995"}
            },
        };

        public static List<string> GetProductIds() => new List<string>()
        {
            "66185655155f38e1afb9fd29",
            "6619511d6f2b5851d852c0d8"
        };

        public static List<ProductCollection> productCollections() => new List<ProductCollection>()
        {
            new ProductCollection
            {
                _id = "6619511d6f2b5851d852c0d8",
                Name = "Name",
                Price = 10000,
                Quantity = 10,
                Description = "Description",
                Category = "Category",
                State = true,
            }
        };

        public static List<WriteModel<ProductCollection>> writeModels() => new List<WriteModel<ProductCollection>>()
        {
            
        };

        public static ProductInCart productInCart() => new ProductInCart()
        {
            _id = "",
            Name = "",
            UnitPrice = 10,
            QuantityInCart = 3
        };
    }
}
