using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Interfaces
{
    public interface ISalesSpecialRepository
    {
        List<SalesSpecials> GetAllSpecials();
        SalesSpecials GetSpecialByID(int specialID);
        void InsertSpecial(SalesSpecials special);
        void DeleteSpecial(int specialID);
    }
}
