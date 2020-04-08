using DvdLibrary.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibrary.Data.Factories
{
    public static class DvdRepositoryFactory
    {
        public static IDvdRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "SampleData":
                    return new DvdRepositoryMock();
                case "ADO":
                    return new DvdRepositoryADO();
                case "EntityFramework":
                    return new DvdRepositoryEF();
                default:
                    throw new Exception("Could not find RepositoryType configuration value.");
            }
        }
    }
}
