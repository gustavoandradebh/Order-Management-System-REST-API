using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        IEnumerable<Customer> GetAll(int pageSize, int pageNumber);
    }
}
