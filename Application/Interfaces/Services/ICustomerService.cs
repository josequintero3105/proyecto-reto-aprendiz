using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces.Services
{
    public interface ICustomerService
    {
        Task CreateCustomer(Customer customer);
        Task UpdateCustomer(Customer customer);
        Task<Customer> GetCustomerById(Customer customer);
    }
}
