using DataAccess.EF.Repositories;
using Domain.Interfaces;

namespace DataAccess.EF.UnitOfWork;

public class UnitOfWork: IUnitOfWork
{
    private readonly ApplicationContext _context;
    public IOrderRepository Orders { get; private set; }
    public IProductRepository Products { get; private set; }

    public UnitOfWork(ApplicationContext context)
    {
        _context = context;
        Orders = new OrderRepository(_context);
        Products = new ProductRepository(_context);
    }

    public int Complete() => _context.SaveChanges();
    public void Dispose() => _context.Dispose();
}
