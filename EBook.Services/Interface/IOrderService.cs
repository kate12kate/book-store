using EBook.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBook.Service.Interface
{
    public interface IOrderService
    {
        List<Order> getAllOrders();
    }
}
