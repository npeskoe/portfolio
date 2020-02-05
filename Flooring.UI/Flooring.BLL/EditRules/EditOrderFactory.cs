using Flooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.BLL.EditRules
{
    public class EditOrderFactory
    {
        public static IEditOrder Create()
        {
            return new EditOrderRules();
        }
    }
}
