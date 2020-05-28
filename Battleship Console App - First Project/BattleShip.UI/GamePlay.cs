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
    class GamePlay
    {
        public static void Startup()
        {
            bool playAgain = true;
            while (playAgain)
            {
                ConsoleOutput.MainMenu();
                Console.Clear();

                Player player1 = new Player();
                Player player2 = new Player();
                Player currentPlayer = new Player();

                player1.Name = ConsoleInput.GetPlayerNames("Player 1 - Please enter your name:");
                Console.Clear();
                player2.Name = ConsoleInput.GetPlayerNames("Player 2 - Please enter your name:");
                Console.Clear();

                Random random = new Random();
                int[] playerList = { 0, 1 };
                int playerSelection = random.Next(0, playerList.Length);

                if (playerSelection == 0)
                {
                    currentPlayer.Name = player1.Name;
                    currentPlayer.Board = player1.Board;
                }
                currentPlayer.Name = player2.Name;
                currentPlayer.Board = player2.Board;

                Console.Clear();

                Console.WriteLine(currentPlayer.Name + " - You will be first to shoot!");
                Console.WriteLine();
                Console.WriteLine(player1.Name + " - Get ready to place your board");
                Console.WriteLine("Press any key to continue...");
                Console.WriteLine();
                Console.ReadKey();
                Console.Clear();

                foreach (ShipType ship in Enum.GetValues(typeof(ShipType)))
                {
                    bool shipPlaced = false;
                    do
                    {
                        Coordinate coordinate = ConsoleInput.GetShipCoordinate(player1.Name + " - Please Enter Your " + ship + " Coordinates. Example B2");
                        ShipDirection direction = ConsoleInput.GetShipDirection(player1.Name + " - Please Enter Your " + ship + "'s Direction - U - Up, D - Down, L - Left, R - Right");
                        PlaceShipRequest placeShipRequest = new PlaceShipRequest()
                        {
                            Coordinate = coordinate,
                            Direction = direction,
                            ShipType = ship
                        };
                        var response = player1.Board.PlaceShip(placeShipRequest);
                        switch (response)
                        {
                            case BLL.Responses.ShipPlacement.NotEnoughSpace:
                                Console.WriteLine("Not enough space");
                                break;

                            case BLL.Responses.ShipPlacement.Overlap:
                                Console.WriteLine("Ship Overlaps");
                                break;
                            default:
                                shipPlaced = true;
                                break;
                        }
                    }
                    while (!shipPlaced);
                }
                Console.Clear();

                foreach (ShipType ship in Enum.GetValues(typeof(ShipType)))
                {
                    bool shipPlaced = false;
                    do
                    {
                        Coordinate coordinate = ConsoleInput.GetShipCoordinate(player2.Name + " - Please Enter Your " + ship + " Coordinates. Example B2");
                        ShipDirection direction = ConsoleInput.GetShipDirection(player2.Name + " - Please Enter Your " + ship + "'s Direction - U - Up, D - Down, L - Left, R - Right");
                        PlaceShipRequest placeShipRequest = new PlaceShipRequest()
                        {
                            Coordinate = coordinate,
                            Direction = direction,
                            ShipType = ship
                        };
                        var response = player2.Board.PlaceShip(placeShipRequest);
                        switch (response)
                        {
                            case BLL.Responses.ShipPlacement.NotEnoughSpace:
                                Console.WriteLine("Not Enough Space");
                                break;
                            case BLL.Responses.ShipPlacement.Overlap:
                                Console.WriteLine("Ship Overlaps");
                                break;
                            default:
                                shipPlaced = true;
                                break;
                        }
                    }
                    while (!shipPlaced);
                }
                Console.Clear();

                Console.WriteLine();
                Console.WriteLine(currentPlayer.Name + " - Get ready to shoot!");

                bool GameOver = false;

                while (!GameOver)
                {
                    if (currentPlayer.Name == player1.Name)
                    {
                        Coordinate fireShotCoordinate = ConsoleInput.FireShotCoordinates(player1.Name + " - Please select coordinates to fire your shot:");
                        FireShotResponse fireShotResponse = new FireShotResponse();
                        fireShotResponse = player2.Board.FireShot(fireShotCoordinate);
                        switch (fireShotResponse.ShotStatus)
                        {
                            case ShotStatus.Duplicate:
                                break;
                            case ShotStatus.Invalid:
                                break;
                            case ShotStatus.Hit:
                                Console.WriteLine("Nice hit!");
                                break;
                            case ShotStatus.Miss:
                                Console.WriteLine("You missed!");
                                break;
                            case ShotStatus.HitAndSunk:
                                Console.WriteLine("Hit and Sink");
                                break;
                            case ShotStatus.Victory:
                            default:
                                Console.WriteLine("Congrats! You Win!");
                                GameOver = true;
                                break;
                        }
                        DisplayBoard.DisplayScoreBoard(player2.Board);
                        Console.WriteLine();
                    }


                    while (!GameOver)
                    {
                        Coordinate fireShotCoordinate2 = ConsoleInput.FireShotCoordinates(player2.Name + " - Please select coordinates to fire your shot:");
                        FireShotResponse fireShotResponse2 = new FireShotResponse();
                        fireShotResponse2 = player1.Board.FireShot(fireShotCoordinate2);
                        switch (fireShotResponse2.ShotStatus)
                        {
                            case ShotStatus.Duplicate:
                                break;
                            case ShotStatus.Invalid:
                                break;
                            case ShotStatus.Hit:
                                Console.WriteLine("Nice hit!");
                                break;
                            case ShotStatus.Miss:
                                Console.WriteLine("You missed!");
                                break;
                            case ShotStatus.HitAndSunk:
                                Console.WriteLine("Hit and Sink");
                                break;
                            case ShotStatus.Victory:
                            default:
                                Console.WriteLine("Congrats! You Win!");
                                GameOver = true;
                                break;
                        }
                        DisplayBoard.DisplayScoreBoard(player1.Board);
                        Console.WriteLine();
                    }
                }

                while (!GameOver)
                {
                    if (currentPlayer.Name == player2.Name)
                    {
                        Coordinate fireShotCoordinate3 = ConsoleInput.FireShotCoordinates(player2.Name + " - Please select coordinates to fire your shot:");
                        FireShotResponse fireShotResponse3 = new FireShotResponse();
                        fireShotResponse3 = player1.Board.FireShot(fireShotCoordinate3);
                        switch (fireShotResponse3.ShotStatus)
                        {
                            case ShotStatus.Duplicate:
                                Console.WriteLine("Duplicate Shot!");
                                break;
                            case ShotStatus.Invalid:
                                Console.WriteLine("Invalid Shot!");
                                break;
                            case ShotStatus.Hit:
                                Console.WriteLine("Nice hit!");
                                break;
                            case ShotStatus.Miss:
                                Console.WriteLine("You missed!");
                                break;
                            case ShotStatus.HitAndSunk:
                                Console.WriteLine("Hit and Sink");
                                break;
                            case ShotStatus.Victory:
                            default:
                                Console.WriteLine("Congrats! You Win!");
                                GameOver = true;
                                break;
                        }
                        DisplayBoard.DisplayScoreBoard(player1.Board);
                        Console.WriteLine();
                    }
                    while (!GameOver)
                    {
                        Coordinate fireShotCoordinate4 = ConsoleInput.FireShotCoordinates(player1.Name + " - Please select coordinates to fire your shot:");
                        FireShotResponse fireShotResponse4 = new FireShotResponse();
                        fireShotResponse4 = player2.Board.FireShot(fireShotCoordinate4);
                        switch (fireShotResponse4.ShotStatus)
                        {
                            case ShotStatus.Duplicate:
                                Console.WriteLine("Duplicate Shot!");
                                break;
                            case ShotStatus.Invalid:
                                Console.WriteLine("Invalid Shot!");
                                break;
                            case ShotStatus.Hit:
                                Console.WriteLine("Nice hit!");
                                break;
                            case ShotStatus.Miss:
                                Console.WriteLine("You missed!");
                                break;
                            case ShotStatus.HitAndSunk:
                                Console.WriteLine("Hit and Sink");
                                break;
                            case ShotStatus.Victory:
                            default:
                                Console.WriteLine("Congrats! You Win!");
                                GameOver = true;
                                break;
                        }
                        DisplayBoard.DisplayScoreBoard(player2.Board);
                        Console.WriteLine();
                    }
                }

                Console.WriteLine("Do you want to play again? Y or N");
                string startOver = Console.ReadLine().ToUpper();
                if (startOver == "Y" || startOver == "YES")
                {
                    playAgain = true;
                }
                if (startOver == "N" || startOver == "NO")
                {
                    Console.WriteLine("Thanks for playing!");
                    playAgain = false;
                }
            }
        }
    }
}
