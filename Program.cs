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
        var inputChain = new InputHandlerChain();
        while(running)
        {

            Console.SetCursorPosition(0, 22);
            ConsoleKey key = Console.ReadKey().Key;
            inputChain.HandleInput(key, player, board, ref running);
        }
    }
}