using CarDealership.Data.Interfaces;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Test
{
    public class IntColorsRepositoryQA : IIntColorsRepository
    {
        public List<IntColors> GetAll()
        { 
                List<IntColors> colors = new List<IntColors>();

                colors.Add(new IntColors { IntColorID = 1, IntColorName = "Black" });
                colors.Add(new IntColors { IntColorID = 2, IntColorName = "White" });
                colors.Add(new IntColors { IntColorID = 3, IntColorName = "Gray" });
                colors.Add(new IntColors { IntColorID = 4, IntColorName = "Tan" });
                colors.Add(new IntColors { IntColorID = 5, IntColorName = "Brown" });

                return colors;
            }
    }
}
