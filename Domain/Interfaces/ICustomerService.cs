using Domain.DTOs;

namespace Domain.Interfaces;

public interface ICustomerService
{
    IEnumerable<CustomerDto> GetAll(int? pageSize, int? pageNumber);
}
