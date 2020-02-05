using Flooring.Models;
using Flooring.Models.Interfaces;
using Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.BLL.CalculateRules
{
    public class CalculateOrderFactory
    {
        public static ICalculateOrder Create()
        {
            return new CalculateOrderRules();            
        }
        
    }
}
