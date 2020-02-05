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
    class DisplayBoard
    {
        public static void DisplayScoreBoard(Board board)
        {
            Console.Write("    "+"1 2 3 4 5 6 7 8 9 10");

            for (int row = 1; row <= 10; row++)
            {
                Console.Write($"\n{(char)(row + 'A'-1)}   ");

                for (int col = 1; col <= 10; col++)
                {
                    switch (board.CheckCoordinate(new Coordinate(col,row)))
                    {
                        case ShotHistory.Hit:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("H ");
                            Console.ResetColor();
                            break;
                        case ShotHistory.Miss:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("M ");
                            Console.ResetColor();
                            break;
                        case ShotHistory.Unknown:
                            Console.Write("X ");
                            break;
                    }

                }
            }
        }
    }
}



