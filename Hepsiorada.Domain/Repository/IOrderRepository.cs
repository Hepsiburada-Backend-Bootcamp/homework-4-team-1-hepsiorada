﻿using Hepsiorada.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Domain.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
        public Task<Order> AddOrderWithDetails(Order order, List<OrderDetail> orderDetailsList);
    }
}
