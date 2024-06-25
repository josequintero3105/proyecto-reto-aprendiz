using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Entries;
using Core.Entities.MongoDB;

namespace Application.Interfaces.Services
{
    public interface ICustomerService
    {
        /// <summary>
        /// Update data from customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<CustomerOutput> UpdateCustomerData(CustomerInput customer, string _id);
        /// <summary>
        /// Get customer by id
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        Task<CustomerOutput> GetCustomerById(string _id);
        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        Task DeleteCustomer(string _id);
        /// <summary>
        /// Create a new customer
        /// </summary>
        /// <param name="customerInput"></param>
        /// <returns></returns>
        Task<CustomerCollection> CreateCustomer(CustomerInput customerInput);
    }
}
