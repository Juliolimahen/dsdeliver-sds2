using DsDelivery.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsDelivery.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> DeleteCustomerAsync(int id);
        Task<Customer> GetCustomerAsync(int id);
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> InsertCustomerAsync(Customer customer);
        Task<Customer> UpdateCustomerAsync(Customer customer);
        void UpdateCustomerTelefones(Customer customer, Customer customerConsulted);
    }
}
