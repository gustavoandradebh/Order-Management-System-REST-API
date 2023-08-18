using Domain.Entities;
using Domain.Interfaces;

namespace DataAccess.EF.Repositories
{
    public class ProductRepository: GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationContext context) : base(context) { }
    }
}
