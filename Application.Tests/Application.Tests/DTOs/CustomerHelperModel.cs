using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Entries;
using Application.DTOs.Responses;
using Core.Entities.MongoDB;

namespace Application.Tests.Application.Tests.DTOs
{
    public static class CustomerHelperModel
    {
        public static CustomerInput GetCustomerForCreation()
        {
            return new CustomerInput
            {
                Name = "name",
                Document = "111",
                DocumentType = "CC",
                Email = "email@gmail.com",
                Phone = "12345"
            };
        }

        public static CustomerInput GetCustomerForCreationOrUpdateWithDocumentEmpty()
        {
            return new CustomerInput
            {
                Name = "name",
                Document = "",
                DocumentType = "CC",
                Email = "email@gmail.com",
                Phone = "12345"
            };
        }

        public static CustomerInput GetCustomerForCreationOrUpdateWithDocumentTypeEmpty()
        {
            return new CustomerInput
            {
                Name = "name",
                Document = "111",
                DocumentType = "",
                Email = "email@gmail.com",
                Phone = "12345"
            };
        }

        public static CustomerInput GetCustomerForCreationOrUpdateWithNameEmpty()
        {
            return new CustomerInput
            {
                Name = "",
                Document = "111",
                DocumentType = "CC",
                Email = "email@gmail.com",
                Phone = "12345"
            };
        }

        public static CustomerInput GetCustomerForCreationOrUpdateWithEmailEmpty()
        {
            return new CustomerInput
            {
                Name = "name",
                Document = "111",
                DocumentType = "CC",
                Email = "",
                Phone = "12345"
            };
        }

        public static CustomerInput GetCustomerForCreationOrUpdateWithPhoneEmpty()
        {
            return new CustomerInput
            {
                Name = "name",
                Document = "111",
                DocumentType = "Cedula",
                Email = "email",
                Phone = ""
            };
        }

        public static CustomerOutput GetCustomerFromMongo()
        {
            return new CustomerOutput
            {
                _id = "6644d3d6a20a7c5dc4ed2680",
                Name = "name",
                Document = "111",
                DocumentType = "CC",
                Email = "email@gmail.com",
                Phone = "12345"
            };
        }

        public static CustomerOutput GetCustomerForDelete()
        {
            return new CustomerOutput
            {
                _id = "6644d3d6a20a7c5dc4ed2680"
            };
        }

        public static CustomerCollection GetCustomerCollection()
        {
            return new CustomerCollection
            {
                _id = "6644d3d6a20a7c5dc4ed2680",
                Name = "name",
                Document = "111",
                DocumentType = "CC",
                Email = "email@gmail.com",
                Phone = "12345"
            };
        }
    }
}
