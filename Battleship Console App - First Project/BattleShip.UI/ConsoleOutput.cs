using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;

namespace BattleShip.UI
{
    class ConsoleOutput
    {
        public static void MainMenu()
        {
            Console.WriteLine("**********************");
            Console.WriteLine("Welcome to BattleShip!");
            Console.WriteLine("**********************");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
           Console.ReadKey();
        }

    }
}
