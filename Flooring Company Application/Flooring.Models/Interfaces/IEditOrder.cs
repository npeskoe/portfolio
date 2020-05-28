﻿using Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models.Interfaces
{
    public interface IEditOrder
    {
        EditOrderResponse EditOrder(Order order, DateTime orderDate, string customerName, string stateName, string productType, decimal area);
    }
}