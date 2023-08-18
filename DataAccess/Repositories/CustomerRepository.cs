using Domain.Entities;
using Domain.Interfaces;

namespace DataAccess.EF.Repositories
{
    public class CustomerRepository: GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationContext context) : base(context) { }

        public IEnumerable<Customer> GetAll(int pageSize, int pageNumber)
        {
            // all customers paginated
            var customers = _context.Customers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            
            return customers;
        }
    }
}
