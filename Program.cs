using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRPG;

internal class Program
{
    static void Main()
    {
        Console.CursorVisible = false;
        Board board = new Board();
        Player player = new Player(board,new Point(1, 1));
        board.StartGame();
        DisplayManager.GetInstance().DisplayGameState(board, player);
        bool running = true;
        while (running)
        {
            Console.SetCursorPosition(0, 22);
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Q:
                    running = false;
                    break;
                case ConsoleKey.W:
                    player.Move(-1, 0);
                    break;
                case ConsoleKey.S:
                    player.Move(1, 0);
                    break;
                case ConsoleKey.D:
                    player.Move(0, 1);
                    break;
                case ConsoleKey.A:
                    player.Move(0, -1);
                    break;
                case ConsoleKey.E:
                    player.PickItem();
                    DisplayManager.GetInstance().DisplayGameState(board, player);
                    break;
                case ConsoleKey.T:
                    player.DropItem();
                    DisplayManager.GetInstance().DisplayGameState(board, player);
                    break;
                case ConsoleKey.G:
                    player.MoveItemFromEqToHand();
                    DisplayManager.GetInstance().DisplayGameState(board, player);
                    break;
                case ConsoleKey.V:
                    player.MoveItemFromHandToEq();
                    DisplayManager.GetInstance().DisplayGameState(board, player);
                    break;

            }
        }

    }
}