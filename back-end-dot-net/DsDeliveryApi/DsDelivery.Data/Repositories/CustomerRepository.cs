using DsDelivery.Core.Domain;
using DsDeliveryApi.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsDelivery.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<Customer> _dbSet;

        public CustomerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Customer>();
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _dbSet
                .Include(p => p.Address)
                .Include(p => p.Phones)
                .AsNoTracking().ToListAsync();
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Address)
                .Include(p => p.Phones)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Customer> InsertCustomerAsync(Customer customer)
        {
            await _dbSet.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            var customerConsulted = await _dbSet
                                                 .Include(p => p.Address)
                                                 .Include(p => p.Phones)
                                                 .FirstOrDefaultAsync(p => p.Id == customer.Id);
            if (customerConsulted == null)
            {
                return null;
            }
            _dbContext.Entry(customerConsulted).CurrentValues.SetValues(customer);
            customerConsulted.Address = customer.Address;
            UpdateCustomerTelefones(customer, customerConsulted);
            await _dbContext.SaveChangesAsync();
            return customerConsulted;
        }

        public void UpdateCustomerTelefones(Customer customer, Customer customerConsulted)
        {
            customerConsulted.Phones.Clear();
            foreach (var telefone in customer.Phones)
            {
                customerConsulted.Phones.Add(telefone);
            }
        }

        public async Task<Customer> DeleteCustomerAsync(int id)
        {
            var customerConsulted = await _dbSet.FindAsync(id);
            if (customerConsulted == null)
            {
                return null;
            }
            var customerRemovido = _dbSet.Remove(customerConsulted);
            await _dbContext.SaveChangesAsync();
            return customerRemovido.Entity;
        }
    }
}
