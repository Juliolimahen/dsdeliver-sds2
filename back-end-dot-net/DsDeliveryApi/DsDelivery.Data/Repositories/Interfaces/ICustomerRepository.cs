using DsDelivery.Core.Domain;

namespace DsDelivery.Data.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        void UpdateCustomerTelefones(Customer customer, Customer customerConsulted);
    }
}
