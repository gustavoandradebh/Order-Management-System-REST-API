using Domain.Interfaces;

namespace DataAccess.EF.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ApplicationContext _context;
    public GenericRepository(ApplicationContext context) => _context = context;

    public void Add(T entity) => _context.Set<T>().Add(entity);
    public IEnumerable<T> GetAll() => _context.Set<T>().ToList();
    public T GetById(int id) => _context.Set<T>().Find(id);
}
