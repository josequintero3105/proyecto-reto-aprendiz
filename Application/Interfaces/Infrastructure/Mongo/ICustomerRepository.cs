using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Responses;
using Core.Entities.MongoDB;

namespace Application.Interfaces.Infrastructure.Mongo
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Defining contract for create product in the database
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<CustomerOutput> CreateCustomerAsync(CustomerOutput customer);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<CustomerOutput> GetCustomerByIdAsync(string _id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        CustomerCollection GetCustomer(CustomerOutput customer);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerToUpdate"></param>
        /// <returns></returns>
        Task<CustomerOutput> UpdateCustomerDataAsync(CustomerOutput customerToUpdate);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<bool> DeleteCustomerAsync(string _id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerToCreate"></param>
        /// <returns></returns>
        Task<CustomerCollection> CreateAsync(CustomerOutput customerToCreate);
    }
}
