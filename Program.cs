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
        Board board = new Board();
        Player player = new Player(board, new Point(1, 1));
        board.StartGame();
        board.DrawBoard(player);
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
                    board.DrawBoard(player);
                    break;
                case ConsoleKey.T:
                    player.DropItem();
                    board.DrawBoard(player);
                    break;
                case ConsoleKey.G:
                    player.MoveItemFromEqToHand();
                    board.DrawBoard(player);
                    break;
                case ConsoleKey.V:
                    player.MoveItemFromHandToEq();
                    board.DrawBoard(player);
                    break;

            }
        }

    }
}