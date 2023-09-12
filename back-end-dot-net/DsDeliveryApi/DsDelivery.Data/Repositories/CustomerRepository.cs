using DsDelivery.Core.Domain;
using DsDelivery.Data.Repositories.Interfaces;
using DsDeliveryApi.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DsDelivery.Data.Repositories;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    protected CustomerRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _dbSet
            .Include(p => p.Address)
            .Include(p => p.Phones)
            .AsNoTracking().ToListAsync();
    }

    public override async Task<Customer> GetByIdAsync(int? id)
    {
        return await _dbSet
            .Include(p => p.Address)
            .Include(p => p.Phones)
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<Customer> UpdateAsync(Customer customer)
    {
        var customerConsulted = await _dbSet
                                             .Include(p => p.Address)
                                             .Include(p => p.Phones)
                                             .FirstOrDefaultAsync(p => p.Id == customer.Id);
        if (customerConsulted == null)
        {
            throw new Exception($"Cliente com Id = {customer.Id} não encontrado.");
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
}
