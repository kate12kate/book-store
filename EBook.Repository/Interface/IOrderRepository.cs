using EBook.Domain;
using EBook.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBook.Repository.Interface
{
    public interface IOrderRepository
    {
        List<Order> getAllOrders();
        public Order getOrderDetails(BaseEntity model);
    }
}
