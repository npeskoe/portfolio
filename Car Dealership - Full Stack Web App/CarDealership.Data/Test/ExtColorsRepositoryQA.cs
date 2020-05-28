using CarDealership.Data.Interfaces;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Test
{
    class ExtColorsRepositoryQA : IExtColorsRepository
    {
        public List<ExtColors> GetAll()
        {
            List<ExtColors> colors = new List<ExtColors>();

            colors.Add(new ExtColors { ExtColorID = 1, ExtColorName = "White" });
            colors.Add(new ExtColors { ExtColorID = 2, ExtColorName = "Black" });
            colors.Add(new ExtColors { ExtColorID = 3, ExtColorName = "Gray" });
            colors.Add(new ExtColors { ExtColorID = 4, ExtColorName = "Silver" });
            colors.Add(new ExtColors { ExtColorID = 5, ExtColorName = "Red" });
            colors.Add(new ExtColors { ExtColorID = 6, ExtColorName = "Blue" });

            return colors;

        }
    }
}
