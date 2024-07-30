using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Entries;
using Application.DTOs.Responses;
using Core.Entities.MongoDB;
using Microsoft.Identity.Client;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Application.Tests.Application.Tests.DTOs
{
    public static class ProductHelperModel
    {
        public static ProductInput GetProductForCreation()
        {
            return new ProductInput
            {
                Name = "Test",
                Price = 10.000,
                Quantity = 1,
                Description = "Test Description",
                Category = "Testing",
                State = true
            };
        }

        public static ProductOutput GetProductFromMongo()
        {
            return new ProductOutput
            {
                _id = "661feb4a110728200e31903e",
                Name = "Test",
                Price = 10000,
                Quantity = 100,
                Description = "Test Description",
                Category = "Testing",
                State = true
            };
        }

        public static ProductCollection GetProductCollection()
        {
            return new ProductCollection
            {
                _id = "661feb4a110728200e31903e",
                Name = "Test",
                Price = 10000,
                Quantity = 100,
                Description = "Test Description",
                Category = "Testing",
                State = true
            };
        }

        public static List<ProductOutput> ListAllProducts() => new List<ProductOutput>
        {
            new ProductOutput
            {
                Name = "Test",
                Price = 0,
                Quantity = 0,
                Description = "Test",
                Category = "Test",
                State = true
            }
        };

        public static List<ProductOutput> ListAllProductsIsEmpty() => new List<ProductOutput>{};
        

        public static ProductInput GetProductForCreationWithProductNameEmpty() => new ProductInput()
        {
            
            Name = "",
            Price = 10.000,
            Quantity = 1,
            Description = "Test Description",
            Category = "Testing",
            State = true
        };

        public static ProductInput GetProductForCreationWithProductNameWrongFormat() => new ProductInput()
        {
            
            Name = "+,-.'?",
            Price = 10.000,
            Quantity = 1,
            Description = "Test Description",
            Category = "Testing",
            State = true
        };

        public static ProductInput GetProductForCreationWithProductDescriptionEmpty() => new ProductInput()
        {
            Name = "Test",
            Price = 10.000,
            Quantity = 1,
            Description = "",
            Category = "Testing",
            State = true
        };

        public static ProductInput GetProductForCreationWithProductDescriptionWrongFormat() => new ProductInput()
        {
            Name = "Test",
            Price = 10.000,
            Quantity = 1,
            Description = "+,-.'?",
            Category = "Testing",
            State = true
        };

        public static ProductInput GetProductForCreationWithProductCategoryEmpty() => new ProductInput()
        {
            Name = "Test",
            Price = 10.000,
            Quantity = 1,
            Description = "Test Description",
            Category = "",
            State = true
        };

        public static ProductInput GetProductForCreationWithProductCategoryWrongFormat() => new ProductInput()
        {
            Name = "Test",
            Price = 10.000,
            Quantity = 1,
            Description = "Test Description",
            Category = "+,-.'?",
            State = true
        };

        public static ProductInput GetProductForCreationWithoutProductPrice() => new ProductInput()
        {
            
            Name = "Test",
            Price = 0,
            Quantity = 1,
            Description = "Test Description",
            Category = "Testing",
            State = true
        };

        public static ProductInput GetProductForUpdateWithProductNameEmpty() => new ProductInput()
        {
            Name = "",
            Price = 10.000,
            Quantity = 1,
            Description = "Test Description",
            Category = "Testing",
            State = true
        };

        public static ProductOutput GetProductForUpdateWithProductNameWrongFormat() => new ProductOutput()
        {
            _id = "661805457b1da8ba4cb52995",
            Name = "+,-.'?",
            Price = 10.000,
            Quantity = 1,
            Description = "Test Description",
            Category = "Testing",
            State = true
        };

        public static ProductInput GetProductForUpdateWithProductDescriptionEmpty() => new ProductInput()
        {
            Name = "Test",
            Price = 10.000,
            Quantity = 1,
            Description = "",
            Category = "Testing",
            State = true
        };

        public static ProductOutput GetProductForUpdateWithProductDescriptionWrongFormat() => new ProductOutput()
        {
            _id = "661805457b1da8ba4cb52995",
            Name = "Test",
            Price = 10.000,
            Quantity = 1,
            Description = "+,-.'?",
            Category = "Testing",
            State = true
        };

        public static ProductInput GetProductForUpdateWithProductCategoryEmpty() => new ProductInput()
        {
            Name = "Test",
            Price = 10.000,
            Quantity = 1,
            Description = "Test Description",
            Category = "",
            State = true
        };

        public static ProductOutput GetProductForUpdateWithProductCategoryWrongFormat() => new ProductOutput()
        {
            _id = "661805457b1da8ba4cb52995",
            Name = "Test",
            Price = 10.000,
            Quantity = 1,
            Description = "Test Description",
            Category = "+,-.'?",
            State = true
        };

        public static ProductOutput GetProductForUpdateWithoutProductPrice() => new ProductOutput()
        {
            _id = "661805457b1da8ba4cb52995",
            Name = "Test",
            Price = 0,
            Quantity = 1,
            Description = "Test Description",
            Category = "Testing",
            State = true
        };
    }
}
