using Domain.DTOs;
using Domain.Interfaces;

namespace Service;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<CustomerDto> GetAll(int? pageSize, int? pageNumber)
    {
        if(!pageSize.HasValue) pageSize = 10;
        if(!pageNumber.HasValue) pageNumber = 1;

        var customers = _unitOfWork.Customers.GetAll(pageSize.Value, pageNumber.Value);
        
        var customerDto = new List<CustomerDto>();

        // TODO: Implement AutoMapper
        foreach (var customer in customers)
        {
            var customerDtoItem = new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email
            };
            customerDto.Add(customerDtoItem);
        }

        return customerDto;
    }
}
