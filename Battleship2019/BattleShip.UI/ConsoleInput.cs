using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.Responses;

namespace BattleShip.UI
{
    public class ConsoleInput
    {

        public static string GetPlayerNames(string message)
        {

            while (true)
            {
                Console.WriteLine(message);
                string userInput = Console.ReadLine();
                if (string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Input is required");
                    continue;
                }
                return userInput;
            }

        }

        public static Coordinate GetShipCoordinate(string message)
        {
            while (true)
            {
                Console.WriteLine(message);

                String userInput = Console.ReadLine().ToUpper();

                if (string.IsNullOrEmpty(userInput) || (userInput.Length == 1) || (userInput.Length > 3))
                {
                    Console.WriteLine(message);
                    continue;
                }

                else
                {

                    int x;
                    int y;
                    x = userInput[0] - 'A'+1;

                    if (int.TryParse(userInput.Substring(1,userInput.Length-1), out y))
                    {
                        //y = int.Parse(userInput.Substring(1));

                        if (x >= 1 && x <= 10 && y >= 1 && y <= 10)
                        {
                            return new Coordinate(y,x);

                        }
                        else
                        {
                            Console.WriteLine("Invalid Coordinates!");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Coordinates!");
                        Console.ReadLine();
                    }
                }
            }
        }
        public static ShipDirection GetShipDirection(string message)
        {
            while (true)
            {
                Console.WriteLine(message);

                switch (Console.ReadLine().ToUpper())
                {
                    case "U":
                        return ShipDirection.Up;
                    case "D":
                        return ShipDirection.Down;
                    case "L":
                        return ShipDirection.Left;
                    case "R":
                        return ShipDirection.Right;
                    default:
                        Console.WriteLine("That is not a valid direction");
                        Console.ReadLine();
                        break;
                }
            }
        }

        public static Coordinate FireShotCoordinates(string message)
        {
            while (true)
            {
                Console.WriteLine(message);

               String userInput = Console.ReadLine().ToUpper();

                if (string.IsNullOrEmpty(userInput) || (userInput.Length == 1) || (userInput.Length > 3))
                {
                    Console.WriteLine(message);
                    continue;
                }

                else
                {

                    int x;
                    int y;
                    x = userInput[0] - 'A'+1;

                    if (int.TryParse(userInput.Substring(1,userInput.Length-1), out y))
                    {
                        //y = int.Parse(userInput.Substring(1));

                        if (x >= 1 && x <= 10 && y >= 1 && y <= 10)
                        {
                            return new Coordinate(y,x);

                        }
                        else
                        {
                            Console.WriteLine("Invalid Coordinates!");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Coordinates!");
                        Console.ReadLine();
                    }
                }
            }
        }
    }
}

                
    











