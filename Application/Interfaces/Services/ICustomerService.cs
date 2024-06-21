using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Entries;

namespace Application.Interfaces.Services
{
    public interface ICustomerService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<CustomerOutput> CreateCustomer(CustomerInput customer);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<CustomerOutput> UpdateCustomerData(CustomerInput customer, string _id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<CustomerOutput> GetCustomerById(string _id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task DeleteCustomer(string _id);
    }
}
