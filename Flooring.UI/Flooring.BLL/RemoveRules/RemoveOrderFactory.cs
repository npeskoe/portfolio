using Flooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.BLL.RemoveRules
{
    class RemoveOrderFactory
    {
        public static IRemoveOrder Create()
        {
            return new RemoveOrderRules();
        }
    }
}
