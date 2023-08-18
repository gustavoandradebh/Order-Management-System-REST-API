using Domain.DTOs;
using Domain.Interfaces;

namespace Service;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public ProductDto? FindById(int id)
    {
        var product = _unitOfWork.Products.GetById(id);

        if (product is null)
            return null;
        
        var productDto = new ProductDto
        {
            Id = product.Id,
            ProductName = product.Name,
            Price = product.Price
        };  

        return productDto;
    }
}