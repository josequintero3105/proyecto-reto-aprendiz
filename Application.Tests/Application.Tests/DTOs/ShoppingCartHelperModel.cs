using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime.SharedInterfaces;
using Application.DTOs.ApiEntities.Output;
using Application.DTOs.Entries;
using Application.DTOs.Responses;
using Core.Entities.MongoDB;
using MongoDB.Driver;
using Application.DTOs.ApiEntities.Input;
using Application.DTOs.ApiEntities.Response;

namespace Application.Tests.Application.Tests.DTOs
{
    public class ShoppingCartHelperModel
    {
        public static List<ShoppingCartCollection> ShoppingCartCollections() => new()
        {
            new ShoppingCartCollection
            {
                _id = "66574ea38d0535a677a3e029",
                ProductsInCart = new List<ProductInCartCollection>
                {
                    new()
                    {
                        _id = "661805457b1da8ba4cb52995",
                        QuantityInCart = 4
                    },
                    new()
                    {
                        _id = "66185655155f38e1afb9fd29",
                        QuantityInCart = 3
                    },
                },
                PriceTotal = 10000,
                Status = "Pending"
            },
            new ShoppingCartCollection
            {
                _id = "",
                ProductsInCart = new List<ProductInCartCollection>
                {
                    new()
                    {
                        _id = "661805457b1da8ba4cb52995",
                        QuantityInCart = 4
                    },
                    new()
                    {
                        _id = "66185655155f38e1afb9fd29",
                        QuantityInCart = 3
                    },
                },
                PriceTotal = 20000,
                Status = "Approved"
            }
        };

        public static ShoppingCartInput GetShoppingCartForCreation()
        {
            return new ShoppingCartInput()
            {
                ProductsInCart = new List<ProductInCart>()
                {
                    new()
                    {
                        _id = "661805457b1da8ba4cb52995",
                        QuantityInCart = 4
                    },
                    new()
                    {
                        _id = "66185655155f38e1afb9fd29",
                        QuantityInCart = 3
                    },
                }
            };
        }

        public static ShoppingCart GetShoppingCartFromMongo() => new()
        {
            _id = "664f40fed44e5362205a9381",
            ProductsInCart = new List<ProductInCart>()
            {
                new()
                {
                    _id = "661805457b1da8ba4cb52995",
                    QuantityInCart = 4
                },
                new()
                {
                    _id = "66185655155f38e1afb9fd29",
                    QuantityInCart = 3
                },
            },
            PriceTotal = 40000,
            Status = "Pending"
        };

        public static ShoppingCartCollection GetShoppingCartCollectionFromMongo() => new()
        {
            _id = "664f40fed44e5362205a9381",
            ProductsInCart = new List<ProductInCartCollection>()
            {
                new()
                {
                    _id = "661805457b1da8ba4cb52995",
                    QuantityInCart = 4
                },
                new()
                {
                    _id = "66185655155f38e1afb9fd29",
                    QuantityInCart = 3
                },
            },
            PriceTotal = 40000,
            Status = "Pending"
        };

        public static ShoppingCart GetShoppingCartForRemoveProducts() => new()
        {
            _id = "6644d077042e0563da8a5600",
            ProductsInCart = new List<ProductInCart>()
            {
                new() {_id = "661805457b1da8ba4cb52995"}
            },
        };

        public static List<string> GetProductIds() => new()
        {
            "66185655155f38e1afb9fd29",
            "6619511d6f2b5851d852c0d8"
        };

        public static List<ProductCollection> ProductCollections() => new()
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

        public static ProductInCart ProductInCart() => new()
        {
            _id = "",
            Name = "",
            UnitPrice = 10,
            QuantityInCart = 3
        };

        public static List<WriteModel<ProductCollection>> WriteModels() => new(){};

        public static TransactionInput TransactionInput() => new()
        {
            Invoice = "66574ea38d0535a677a3e028",
            Description = "Description",
            PaymentMethod = new PaymentMethod()
            {
                PaymentMethodId = 1,
                BankCode = 1077,
                UserType = 0
            },
            Sandbox = new Sandbox()
            {
                IsActive = false,
                Status = "Pending"
            },
            Currency = "COP",
            Value = 10000,
            UrlResponse = "https://8266-179-15-14-38.ngrok.io/prueba",
            UrlConfirmation = "MyWebApiConfirmation",
            MethodConfirmation = "POST",
            Client = new Client()
            {
                DocType = "CC",
                Document = "1000755422",
                Name = "José Manuel",
                LastName = "Quintero Rodríguez",
                Email = "test@test.com",
                IndCountry = "57",
                Phone = "32257522577",
                Country = "CO",
                City = "Medellin",
                Address = "Crr 46",
                IpAddress = "192.168.1.1"
            }
        };

        public static TransactionOutput TransactionOutput() => new()
        {
            _id = "66a1511084783ca7c052634b",
            Invoice = "66574ea38d0535a677a3e028",
            StoreId = "5d9b85f784c9d000019a9bff",
            VendorId = "5d9b6bd284c9d000019a9bfd",
            Description = "Description",
            PaymentMethod = new PaymentMethodOutput()
            {
                PaymentMethodId = "1",
                BankCode = 1077,
                BankName = "Name",
            },
            TransactionStatus = "PendingForPaymentMethod",
            Currency = "COP",
            Value = 10000,
            Sandbox = new SandboxInactive()
            {
                Status = "Pending"
            },
            CreationDate = DateTime.Now,
            PaymentMethodResponse = new PaymentMethodDetailResponse()
            {
                TransactionId = "1",
                StatusResponse = "PendingForPaymentMethod",
                CodeResponse = "3",
                Description = "Description",
                AuthorizationCode = "3",
                ApprovalCode = "0",
                Receipt = "0"
            },
            UrlConfirmation = "https://localhost:7083/api/Transaction/Confirm",
            UrlResponse = "https://8266-179-15-14-38.ngrok.io/prueba",
            MethodConfirmation = "POST"
        };

        public static TransactionResponse TransactionResponse() => new()
        {
            _id = "66b686b2023d9e0f8b2a6846",
            Invoice = "66b686a30f620bb30aa0aa80",
            StoreId = "6099675acae5400001387631",
            VendorId = "609966e0a39cd000012cc490",
            Description = "Description",
            PaymentMethod = new PaymentMethodOutput()
            {
                PaymentMethodId = "1",
                BankCode = 1077,
                BankName = "BANKA"
            },
            TransactionStatus = "Rejected",
            Currency = "COP",
            Value = 10000,
            Sandbox = new SandboxInactive()
            {
                Status = "Pending"
            },
            CreationDate = DateTime.Now,
            PaymentMethodResponse = new PaymentMethodResponse()
            {
                TransactionId = "66b686b2023d9e0f8b2a6846",
                DriverId = "66b686b52b2b2d6bc98232fa",
                StatusResponse = "Pending",
                CodeResponse = "5",
                Description = "Description",
                AuthorizationCode= "4299141",
                ApprovalCode = "0",
                Receipt = "66b686a30f620bb30aa0aa80",
                PaymentRedirectUrl = "https://registro.desarrollo.pse.com.co/PSENF/index.html?enc=PVhiEwh69xvY9YfF6Jwlj%2bPDzmG%2fOtLYNaPTA7W%2b8eg%3d",
            },
            UrlConfirmation = "https://localhost:7083/api/Transaction/Confirm",
            UrlResponse = "https://8266-179-15-14-38.ngrok.io/prueba",
            MethodConfirmation = "POST"
        };
    }
}
