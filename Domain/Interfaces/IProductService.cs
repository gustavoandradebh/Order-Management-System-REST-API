using Domain.DTOs;

namespace Domain.Interfaces
{
    public interface IProductService
    {
        ProductDto? FindById(int id);
    }
}
