using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Core.Entities.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Application.Tests.Application.Tests.DTOs
{
    public static class ProductCollectionHelperModel
    {
        public static ProductCollection GetProductForUpdate()
        {
            string idString = "661805457b1da8ba4cb52995";
            return new ProductCollection
            {
                _id = ObjectId.Parse(idString),
                Name = "Test",
                Price = 10.000,
                Quantity = 1,
                Description = "Test Description",
                Category = "Testing",
                State = true
            };
        }
    }
}
