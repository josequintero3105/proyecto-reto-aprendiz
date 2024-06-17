using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Tests.Application.Tests.DTOs
{
    public static class CustomerHelperModel
    {
        public static Customer GetCustomerForCreation()
        {
            return new Customer
            {
                Name = "name",
                Email = "email",
                Phone = "phone"
            };
        }

        public static Customer GetCustomerForCreationOrUpdateWithNameEmpty()
        {
            return new Customer
            {
                Name = "",
                Email = "email",
                Phone = "phone"
            };
        }

        public static Customer GetCustomerForCreationOrUpdateWithEmailEmpty()
        {
            return new Customer
            {
                Name = "name",
                Email = "",
                Phone = "phone"
            };
        }

        public static Customer GetCustomerForCreationOrUpdateWithPhoneEmpty()
        {
            return new Customer
            {
                Name = "name",
                Email = "email",
                Phone = ""
            };
        }

        public static Customer GetCustomerForUpdate()
        {
            return new Customer
            {
                _id = "6644d3d6a20a7c5dc4ed2680",
                Name = "name",
                Email = "email",
                Phone = "phone"
            };
        }

        public static Customer GetCustomerForDelete()
        {
            return new Customer
            {
                _id = "6644d3d6a20a7c5dc4ed2680"
            };
        }
    }
}
