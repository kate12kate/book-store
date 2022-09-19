using EBook.Domain;
using EBook.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBook.Service.Interface
{
    public interface IOrderService
    {
        List<Order> getAllOrders(string userId);
        public Order getOrderDetails(BaseEntity model);
    }
}
