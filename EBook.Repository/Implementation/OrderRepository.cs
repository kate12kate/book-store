using EBook.Domain.DomainModels;
using EBook.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBook.Repository.Implementation
{
    public class OrderRepository :IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }

        public List<Order> getAllOrders()
        {
            return entities.
                Include(z=>z.Book).
                Include("Book.")
              
                ToListAsync().Result;
        }
    }
}
