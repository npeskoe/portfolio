using Flooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.BLL.AddRules
{
    public class AddOrderFactory
    {
        public static IAddOrder Create()
        {
            return new AddOrderRules();
        }
    }
}
