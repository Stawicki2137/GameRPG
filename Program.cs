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
        Player player = new Player("Player1",board);
        board.Initialize();
        board.UpdateBoard(player);

        int exit = 1;
        while (exit == 1)
        {
            board.DrawBoard(player);
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Q:
                    exit = 0; break;
                case ConsoleKey.W:
                    player.Move(-1, 0);
                    break;
                case ConsoleKey.S:
                    player.Move(1, 0);
                    break;
                case ConsoleKey.A:
                    player.Move(0, -1);
                    break;
                case ConsoleKey.D:
                    player.Move(0, 1);
                    break;
                case ConsoleKey.E:
                    player.PickItem();
                    break;

            }

            board.UpdateBoard(player);


        }
    }
}