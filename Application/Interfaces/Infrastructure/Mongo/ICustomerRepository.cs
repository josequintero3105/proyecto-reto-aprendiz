using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces.Infrastructure.Mongo
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Defining contract for create product in the database
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<Customer> CreateCustomerAsync(Customer customer);
    }
}
