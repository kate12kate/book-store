﻿using EBook.Domain.DomainModels;
using EBook.Repository.Interface;
using EBook.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBook.Service.Implementation
{
   
    public class OrderService:IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        public List<Order> getAllOrders()
        {
            return this._orderRepository.getAllOrders();
        }
    }
}
