﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IOrderRepository Orders { get; }
        IProductRepository Products { get; }
        ICustomerRepository Customers { get; }
        int Complete();
    }
}
